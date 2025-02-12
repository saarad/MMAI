using MongoDB.Driver;
using dataAccess.Models;
using dataAccess.Interfaces;

namespace dataAccess
{
    //todo: Remove string injection in constructor (use option wrapper or IConfiguration)
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private IMongoDatabase _db;
        private MongoClient _client;

        public DbConnectionFactory(string connectionString, string db)
        {
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(db);
        }

        /// <summary>
        /// Gets the MongoDB collection of fighters.
        /// </summary>
        /// <returns>Returns the MongoDB collection of fighters.</returns>
        public IMongoCollection<Fighter> GetInstanceFighterCollection()
        {

            return _db.GetCollection<Fighter>("fighters");
            
        }

        /// <summary>
        /// Gets the MongoDB collection of upcoming fights.
        /// </summary>
        /// <returns>Returns the MongoDB collection of upcoming fights.</returns>
        public IMongoCollection<UpcomingFight> GetInstanceUpcomingFightsCollection()
        {
            return _db.GetCollection<UpcomingFight>("upcoming_fights");
        }

        /// <summary>
        /// Gets the MongoDB collection of match history.
        /// </summary>
        /// <returns>Returns the MongoDB collection of match history.</returns>
        public IMongoCollection<MatchHistory> GetInstanceMatchHistoryCollection()
        {

            return _db.GetCollection<MatchHistory>("match_history");
            
        }

        public IMongoCollection<DataUpdated> GetInstanceUpdatedDataCollection()
        {
            return _db.GetCollection<DataUpdated>("updated_data");
        }
    }
}
