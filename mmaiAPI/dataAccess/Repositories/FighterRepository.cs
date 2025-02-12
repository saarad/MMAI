using dataAccess.Interfaces;
using dataAccess.Models;
using MongoDB.Driver;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;

namespace dataAccess.Repositories
{
    //todo: Make GetAllFighters async for better memory utilization (freeing up threads)
    public class FighterRepository : Repository, IFighterRepository
    {

        public FighterRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {

        }

        /// <summary>
        /// Gets a specific fighter and his stats from mongo db.
        /// </summary>
        /// <param name="name">This is the full name of the fighter, does not include nickname.</param>
        /// <returns>All available stats about the fighter, including his previosu matches.</returns>
        public Fighter GetOneFighter(string name)
        {
            var db_connection = _dbFactory.GetInstanceFighterCollection();
            var db_match_history = _dbFactory.GetInstanceMatchHistoryCollection();
            return db_connection.Find(athl => athl.Name == name).FirstOrDefault();
            
        }


        /// <summary>
        /// Gets all the fighters available in MongoDB.
        /// </summary>
        /// <returns>A list of all the available fighters with fighterimages.</returns>
        public IEnumerable<Fighter> GetAllFighters()
        {
                var db_connection = _dbFactory.GetInstanceFighterCollection();
                var db_match_history = _dbFactory.GetInstanceMatchHistoryCollection();
                var fighters = db_connection.Find(fighter => true).ToList();

                foreach (var fighter in fighters)
                {
                    if (fighter.Image != null)
                    {
                        fighter.ImageString = fighter.Image.First();
                    }
                    else
                    {
                        fighter.ImageString = "https://i.imgur.com/xOj79XZ.png";
                    }
                }
                return fighters;
        
        }


        /// <summary>
        /// Created a fighter by inserting the given fighter into the database.
        /// </summary>
        /// <param name="fighter">This is a fighter object containing information about the new fighter.</param>
        /// <returns>Returns the fighter if successfull, otherwise it will return null.</returns>
        public Fighter CreateOneFighter(Fighter fighter)
        {
            var db_connection = _dbFactory.GetInstanceFighterCollection();
            db_connection.InsertOne(fighter);
            return fighter;
        }
    }
}
