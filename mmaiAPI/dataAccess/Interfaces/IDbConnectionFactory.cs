using MongoDB.Driver;
using dataAccess.Models;

namespace dataAccess.Interfaces
{
    public interface IDbConnectionFactory
    {
        IMongoCollection<Fighter> GetInstanceFighterCollection();
        IMongoCollection<UpcomingFight> GetInstanceUpcomingFightsCollection();
        IMongoCollection<MatchHistory> GetInstanceMatchHistoryCollection();
        IMongoCollection<DataUpdated> GetInstanceUpdatedDataCollection();
    }
}
