using System.Collections.Generic;

namespace dataAccess.Models
{
    public class UpcomingEvent
    {
        public string? Event { get; set; }
        public string? Location { get; set; }
        public string? Date { get; set; }
        public List<UpcomingFight> Fights {get; set;}

        public UpcomingEvent()
        {
            Fights = new List<UpcomingFight>();
        }
    }
}
