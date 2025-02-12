using dataAccess.Interfaces;
using dataAccess.Models;
using MongoDB.Driver;
namespace dataAccess.Repositories
{
    //todo: Make async for better memory utilization (freeing up threads)
    //todo: UpcomingEvent is not a DB-model and could just be a DTO/Type and method extracted to service level (mapping UpcomingFight to UpcomingEvent)
    public class UpcomingEventRepository : Repository, IUpcomingEventRepository
    {
        public UpcomingEventRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {

        }
       
        
        /// <summary>
        /// Gets all the upcoming UFC events and information about each event such as all the fights, time and place.
        /// </summary>
        /// <returns>Returns a list of upcoming events and information about each event.</returns>
        public IEnumerable<UpcomingEvent> GetUpcomingEvents() 
        {
            var db_connection = _dbFactory.GetInstanceUpcomingFightsCollection();
            var upcomingFights = db_connection.Find(fight => true).ToList();

            var upcomingEvents = new List<UpcomingEvent>();


            foreach (var fight in upcomingFights)
            {

                if (!upcomingEvents.Any(f => f.Event == fight.Event))
                { //Create each event
                    var newEvent = new UpcomingEvent();
                    newEvent.Event = fight.Event;
                    newEvent.Date = fight.Date;
                    newEvent.Location = fight.Location;
                    upcomingEvents.Add(newEvent);

                }
            }
            foreach (var upevent in upcomingEvents)
            {
                foreach (var fight in upcomingFights)
                {
                    if (upevent.Event == fight.Event && upevent != null)
                    {
                        upevent.Fights.Add(fight);
                        //todo: Abstract logic for field conversions to an Assembler so it can be reused.
                        if (fight.RedImage != null)
                        {
                            fight.RedImageString = fight.RedImage.FirstOrDefault();
                        }
                        else
                        {
                            fight.RedImageString = "https://i.imgur.com/SDXM8dC.png";
                        }
                        if (fight.BlueImage != null)
                        {
                            fight.BlueImageString = fight.BlueImage.FirstOrDefault();
                        }
                        else
                        {
                            fight.BlueImageString = "https://i.imgur.com/xOj79XZ.png";
                        }
                    }
                }
            }

            return upcomingEvents;
            
        }
    }
}
