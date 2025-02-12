using AutoMapper;
using graphQLService.Services.Interfaces;
using dataAccess.Interfaces;
using graphQLService.DTOs.GraphQLTypes;
using graphQLService.Helpers;
using graphQLService.HttpClients.MLEngine;

namespace graphQLService.Services
{
    public class UpcomingEventService : IUpcomingEventService
    {
        private readonly IMapper _mapper;
        private readonly IUpcomingEventRepository _upcomingEventRepository;
        private readonly MLEngineHttpClient _client;

        public UpcomingEventService(IMapper mapper, IUpcomingEventRepository upcomingEventRepository, MLEngineHttpClient client)
        {
            _mapper = mapper;
            _upcomingEventRepository = upcomingEventRepository;
            _client = client;
        }

        public IEnumerable<UpcomingEventType> GetUpcomingEvents()
        {
            var upcomingEvents = _upcomingEventRepository.GetUpcomingEvents().OrderBy(d => MmaiDateConverter.ConvertDateFullNameStringToDateTime(d.Date));
            return _mapper.Map<IEnumerable<UpcomingEventType>>(upcomingEvents);
        }

        //might be redundant, or could be in a separate rest api
        public async Task ScrapeUpcomingEvents()
        {
            await _client.ScrapeUpcoming();
        }
    }
}
