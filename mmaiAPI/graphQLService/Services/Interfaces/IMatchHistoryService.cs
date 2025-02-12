using graphQLService.Services.Interfaces.DependencyInjection;
using graphQLService.DTOs.GraphQLTypes;

namespace graphQLService.Services.Interfaces
{
    public interface IMatchHistoryService : ITransient
    {
        IEnumerable<MatchHistoryType> GetMatchHistoryFighter(string fighterName);
        IEnumerable<CompletedEventType> GetCompletedEvents();
        IEnumerable<MatchHistoryType> GetOneCompletedEvent(string date, string eventName);
    }
}
