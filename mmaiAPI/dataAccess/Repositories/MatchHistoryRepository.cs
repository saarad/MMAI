using dataAccess.Interfaces;
using dataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace dataAccess.Repositories
{
    //todo: Make methods async for better memory utilization (async will free up threads while waiting for db to answer)
    public class MatchHistoryRepository : Repository, IMatchHistoryRepository
    {
        private readonly IMongoCollection<MatchHistory> _matchHistoryCollection;
        public MatchHistoryRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
            _matchHistoryCollection = _dbFactory.GetInstanceMatchHistoryCollection();
        }

        public IEnumerable<MatchHistory> GetCompletedMatchesForFighter(string fighterName)
        {
            return _matchHistoryCollection
                .Find(history => (history.RedName == fighterName || history.BlueName == fighterName))
                .ToList() ?? new List<MatchHistory>();
        }

        /// <summary>
        /// Gets event name and date for all completed matches
        /// </summary>
        /// <returns>The events</returns>
        public IEnumerable<BsonDocument> GetAllMatchesNamesAndDate()
        {
            var db_connection = _dbFactory.GetInstanceMatchHistoryCollection();
            var projection = Builders<MatchHistory>.Projection //Select EventName,Date from ...
                .Include("EventName")
                .Include("Date")
                .Exclude("_id");
            return db_connection.Find(fight => true).Project(projection).ToList();
        }

        /// <summary>
        /// Gets the fights of one event and organizes them
        /// </summary>
        /// <param name="eventname"></param> Name of the event
        /// <param name="date"></param> Date of the event
        /// <returns>The fights of the event</returns>
        public IEnumerable<MatchHistory> GetOneCompletedEvent(string eventname, string date)
        {
            var completedFights = _matchHistoryCollection.Find(fight => fight.EventName == eventname && fight.Date == date).ToList();
            return completedFights;
        }
    }
}
