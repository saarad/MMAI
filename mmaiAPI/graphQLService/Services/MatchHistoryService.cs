using graphQLService.Assemblers.Interfaces;
using graphQLService.Services.Interfaces;
using dataAccess.Interfaces;
using graphQLService.DTOs.GraphQLTypes;
using graphQLService.Helpers;
using AutoMapper;

namespace graphQLService.Services
{
    public class MatchHistoryService : IMatchHistoryService
    {
        private readonly IMatchHistoryRepository _matchHistoryRepository;
        private readonly IMatchHistoryAssembler _matchHistoryAssembler;
        private readonly IMapper _mapper;
        public MatchHistoryService(IMatchHistoryRepository matchHistoryRepository, IMatchHistoryAssembler matchHistoryAssembler, IMapper mapper)
        {
            _matchHistoryRepository = matchHistoryRepository;
            _matchHistoryAssembler = matchHistoryAssembler;
            _mapper = mapper;
        }

        public IEnumerable<CompletedEventType> GetCompletedEvents()
        {
            var allMatchesGroupedByName = _matchHistoryRepository.GetAllMatchesNamesAndDate().GroupBy(d => d["EventName"].AsString);
            var completedEvents = new List<CompletedEventType>();
            foreach(var group in allMatchesGroupedByName)
            {
                completedEvents.Add(new CompletedEventType()
                {
                    EventName = group.Key,
                    Date = group.First()["Date"].AsString
                });
            }

            return completedEvents.OrderByDescending(d => MmaiDateConverter.ConvertMatchDateFormat(d.Date));
        }

        public IEnumerable<MatchHistoryType> GetMatchHistoryFighter(string fighterName)
        {
            var matchHistory = _matchHistoryRepository.GetCompletedMatchesForFighter(fighterName);

            var s = _matchHistoryAssembler.WriteMatchHistoryToGraphQLType(matchHistory);

            return s;
        }

        public IEnumerable<MatchHistoryType> GetOneCompletedEvent(string date, string eventName)
        {
            var matchesInEvent = _matchHistoryRepository.GetOneCompletedEvent(eventName, date);

            return _matchHistoryAssembler.WriteMatchHistoryToGraphQLType(matchesInEvent);
        }
    }
}
