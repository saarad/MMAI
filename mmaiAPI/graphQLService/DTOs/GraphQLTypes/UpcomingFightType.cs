using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphQLService.DTOs.GraphQLTypes
{
    public class UpcomingFightType
    {
        public string? EventName { get; set; }
        public string? Date { get; set; }
        public string? Location { get; set; }
        public string? Fighter1 { get; set; }
        public string? Fighter2 { get; set; }
        public string? WeightClass { get; set; }
        public string? RedImage { get; set; }
        public string? BlueImage { get; set; }
    }
}
