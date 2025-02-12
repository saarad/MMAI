using System;
using System.Collections.Generic;
using System.Text;

namespace dataAccess.Models
{
    public class CompletedEvent
    {
        public string? Event { get; set; }
        public string? Date { get; set; }
        public List<MatchHistory> Fights { get; set; }

        public CompletedEvent()
        {
            Fights = new List<MatchHistory>(); //to avoid nullpointer exception
        }
    }
}
