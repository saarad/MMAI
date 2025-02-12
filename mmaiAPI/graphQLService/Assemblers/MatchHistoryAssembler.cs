using graphQLService.DTOs.GraphQLTypes;
using AutoMapper;
using graphQLService.Helpers;
using dataAccess.Models;
using graphQLService.Assemblers.Interfaces;

namespace graphQLService.Assemblers
{
    public class MatchHistoryAssembler : IMatchHistoryAssembler
    {
        private readonly IMapper _mapper;

        public MatchHistoryAssembler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<MatchHistoryType> WriteMatchHistoryToGraphQLType(IEnumerable<MatchHistory> matchHistory)
        {
            foreach (var match in matchHistory)
            {
                if (match.RedResult == "W")
                {
                    match.Winner = "Red";
                }
                else
                {
                    match.Winner = "Blue";
                }
                if (match.RedImage != null)
                {
                    match.RedImageString = match.RedImage.First();
                }
                else
                {
                    match.RedImageString = "https://i.imgur.com/xOj79XZ.png";
                }
                if (match.BlueImage != null)
                {
                    match.BlueImageString = match.BlueImage.First();
                }
                else
                {
                    match.BlueImageString = "https://i.imgur.com/xOj79XZ.png";
                }
                if (match.Date != null)
                {
                    match.Date = MmaiDateConverter.ConvertMatchDateFormat(match.Date);
                }
                else //no order if date is missing, try placing them last
                {
                    match.Date = MmaiDateConverter.ConvertMatchDateFormat("December 22, 1900");
                }

            }
            matchHistory = matchHistory.OrderByDescending(match => match.Date).ToList();

            return _mapper.Map<IEnumerable<MatchHistoryType>>(matchHistory);
        }
    }
}
