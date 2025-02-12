using graphQLService.Services.Interfaces.DependencyInjection;
using graphQLService.DTOs.GraphQLTypes;

namespace graphQLService.Services.Interfaces
{
    public interface IUpcomingEventService : ITransient
    {
        IEnumerable<UpcomingEventType> GetUpcomingEvents();
        Task ScrapeUpcomingEvents();
    }
}
