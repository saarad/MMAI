using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphQLService.DTOs.GraphQLTypes.InputTypes
{
    public class UpcomingFightInputType
    {
        public int Id { get; set; }
        public string Event { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Record1 { get; set; }
        public string Record2 { get; set; }
        public string WeightClass { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
