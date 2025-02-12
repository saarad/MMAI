using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphQLService.DTOs.GraphQLTypes;
using graphQLService.Services.Interfaces;

namespace graphQLService.Schema
{
    /// <summary>
    /// Treat as controller methods (readonly)
    /// </summary>
    public class Query
    {
        private readonly IFighterService _fighterService;
        private readonly IUpcomingEventService _upcomingEventService;
        private readonly IMatchHistoryService _matchHistoryService;

        public Query(IFighterService fighterService,
                     IUpcomingEventService upcomingEventService,
                     IMatchHistoryService matchHistoryService)
        {
            _fighterService = fighterService;
            _upcomingEventService = upcomingEventService;
            _matchHistoryService = matchHistoryService;
        }

        public FighterType GetOneFighter(string name)
        {
            return _fighterService.GetOneFighter(name);
        }

        public IEnumerable<FighterType> GetAllFighters()
        {
            return _fighterService.GetAllFighters();
        }

        public async Task<PredictedWinnerType> GetPredictedWinner(string redName, string blueName)
        {
            return await _fighterService.GetPredictedWinner(redName, blueName);
        }

        public IEnumerable<UpcomingEventType> GetUpcomingEvents()
        {
            return _upcomingEventService.GetUpcomingEvents();
        }

        public IEnumerable<CompletedEventType> GetCompletedEvents()
        {
            return _matchHistoryService.GetCompletedEvents();
        }

        public IEnumerable<MatchHistoryType> GetOneCompletedEvent(string eventName, string date)
        {
            return _matchHistoryService.GetOneCompletedEvent(date, eventName);
        }
    }
}
