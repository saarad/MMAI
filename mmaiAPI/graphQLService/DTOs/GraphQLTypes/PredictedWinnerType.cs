using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphQLService.DTOs.GraphQLTypes
{
    public class PredictedWinnerType
    {
        public string Name { get; set; }
        public double Prob { get; set; }
    }
}
