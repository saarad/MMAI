using dataAccess.Models;

namespace dataAccess.Interfaces
{
    public interface IUpcomingEventRepository : IRepository
    {
        IEnumerable<UpcomingEvent> GetUpcomingEvents();
    }
}
