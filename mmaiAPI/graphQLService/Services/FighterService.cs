using AutoMapper;
using graphQLService.Services.Interfaces;
using dataAccess.Interfaces;
using graphQLService.DTOs.GraphQLTypes;
using graphQLService.DTOs.MLEngine;
using graphQLService.Exceptions;
using graphQLService.Helpers;
using graphQLService.HttpClients.MLEngine;

namespace graphQLService.Services
{
    //todo: move business logic from repository to service
    public class FighterService : IFighterService
    {
        private readonly IFighterRepository _fighterRepository;
        private readonly IMapper _mapper;
        private readonly IMatchHistoryService _matchHistoryService;
        private readonly MLEngineHttpClient _client;

        public FighterService
            (IFighterRepository fighterRepository, 
            IMapper mapper, 
            IMatchHistoryService matchHistoryService, 
            MLEngineHttpClient client)
        {
            _fighterRepository = fighterRepository;
            _mapper = mapper;
            _matchHistoryService = matchHistoryService;
            _client = client;
        }

        public IEnumerable<FighterType> GetAllFighters()
        {
            var fighters = _fighterRepository.GetAllFighters();
            return _mapper.Map<IEnumerable<FighterType>>(fighters);
        }

        public FighterType GetOneFighter(string name)
        {
            var fighter = _fighterRepository.GetOneFighter(name) ?? throw new NotFoundException($"No fighter with name {name} is found");
            var matchHistory = _matchHistoryService.GetMatchHistoryFighter(name);

            //todo: Abstract logic for field conversions to an Assembler so it can be reused.
            if (!fighter.Birth.Equals("--"))
            {
                var age = MmaiCalculator.CalculateAge(MmaiDateConverter.ConvertDateStringToDateTime(fighter.Birth));
                fighter.Birth = age;
            }
            if (!fighter.Height.Equals(""))
            {
                fighter.HeightCm = MmaiCalculator.GetHeightMetric(fighter.Height);
            }
            if (fighter.Weight != null)
            {
                fighter.WeightKg = MmaiCalculator.GetWeightKg(fighter.Weight);
            }
            if (fighter.Reach != null)
            {
                fighter.ReachCm = MmaiCalculator.GetReachMetric(fighter.Reach);
            }
            if (fighter.Weight != null)
            {
                var weightClass = MmaiCalculator.CalculateWeightclass(fighter.Weight);
                fighter.WeightClass = weightClass;
            }
            if (fighter.Image != null)
            {
                fighter.ImageString = fighter.Image.First();
            }
            else
            {
                fighter.ImageString = "https://i.imgur.com/xOj79XZ.png";
            }

            var fighterType = _mapper.Map<FighterType>(fighter);
            fighterType.MatchHistory = matchHistory;

            return fighterType;
        }


        /// The method does a GET-call to the prediction API and returns the outcome.
        /// </summary>
        /// <param name="redName">The full name of the favourite fighter.</param>
        /// <param name="blueName">The full name of the underdog fighter.</param>
        /// <returns>Returns a prediction object containing the predicted winner's name and the pobability of the predicted outcome.</returns>
        public async Task<PredictedWinnerType> GetPredictedWinner(string redName, string blueName)
        {
            if (redName == blueName && redName != "" && blueName != "")
            {
                var defaultPred = new PredictedWinnerType();
                defaultPred.Name = "Draw";
                defaultPred.Prob = 50;
                return defaultPred;
            }
            try
            {

                var predictedWinner = await _client.GetPredictedWinner(new MatchupRequest() { fighter1 = redName, fighter2 = blueName });

                //reduce all probability over 65% with half (todo: reduce less when model is improved)
                var probability = predictedWinner.prob * 100;
                if (probability > 65)
                {
                    double probOver65 = probability - 65;
                    probOver65 /= 2;
                    probability = (65 + probOver65);
                }

                predictedWinner.prob = Math.Round(probability, 1);


                return _mapper.Map<PredictedWinnerType>(predictedWinner);
            }
            catch (Exception e)
            {
                throw new MLEngineException(e.Message, e);
            }
        }
    }
}
