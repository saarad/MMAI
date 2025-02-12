using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphQLService.DTOs.MLEngine
{
    public class PredictedWinnerResponse
    {
        public string name { get; set; }
        public double prob { get; set; }
    }
}
