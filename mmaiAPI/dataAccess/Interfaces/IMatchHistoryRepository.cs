using dataAccess.Models;
using MongoDB.Bson;

namespace dataAccess.Interfaces
{
    public interface IMatchHistoryRepository : IRepository
    {
        IEnumerable<MatchHistory> GetOneCompletedEvent(string eventname, string date);
        IEnumerable<MatchHistory> GetCompletedMatchesForFighter(string fighterName);
        IEnumerable<BsonDocument> GetAllMatchesNamesAndDate();
    }
}
