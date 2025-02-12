using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphQLService.DTOs.GraphQLTypes
{
    public class FighterType
    {
        public string? Name { get; set; }
        public string? Nickname { get; set; }
        public string? Height { get; set; }
        public double HeightCm { get; set; }
        public int? Weight { get; set; }
        public double WeightKg { get; set; }
        public string? WeightClass { get; set; }
        public int? Reach { get; set; }
        public double? ReachCm { get; set; }
        public string? Stance { get; set; }
        public string? Birth { get; set; }
        public double? Slpm { get; set; } //Significant strikes landed per minute
        public int? StrikesAccuracy { get; set; } //Significant strike accuracy
        public double? Sapm { get; set; } //Significant strikes absorbed per minute
        public double? StrikesDefence { get; set; } //Significant strike defence
        public double? TakedownAverage { get; set; } //Average takedown landed per 15 minutes
        public int? TakedownAccuracy { get; set; }
        public int? TakedownDefence { get; set; } //Precentage of takedown attempts from oponent failed
        public double? SubmissionAverage { get; set; } //Average submissions attempted per 15 minutes
        public string? Record { get; set; }
        public string?[] ImageArray { get; set; }
        public string? Image { get; set; }
        public IEnumerable<MatchHistoryType> MatchHistory { get; set; }
    }
}
