using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphQLService.HttpClients;
using graphQLService.DTOs.GraphQLTypes;
using graphQLService.DTOs.MLEngine;
using graphQLService.Exceptions;

namespace graphQLService.HttpClients.MLEngine
{
    public class MLEngineHttpClient : MmaiHttpClient
    {
        private const string GetPredictionPath = "get-prediction";
        private const string ScrapeUpcomingPath = "scrape-upcoming";
        public MLEngineHttpClient(HttpClient client) : base(client)
        {
        }

        public async Task<PredictedWinnerResponse?> GetPredictedWinner(MatchupRequest matchup)
        {
            try
            {
                return await Post<PredictedWinnerResponse>(GetPredictionPath, matchup);
            }
            catch(Exception e)
            {
                throw new MLEngineException("Couldn't get prediction. Error " + e);
            }
        }

        public async Task ScrapeUpcoming()
        {
            await Post<object>(ScrapeUpcomingPath, null);
        }
    }
}
