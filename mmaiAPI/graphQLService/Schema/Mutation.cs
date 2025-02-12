using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphQLService.Services.Interfaces;


namespace graphQLService.Schema
{
    /// <summary>
    /// Treat as controller methods (write)
    /// </summary>
    public class Mutation
    {
        private readonly IMessageService _messageService;
        private readonly IUpcomingEventService _upcomingEventService;

        public Mutation(IMessageService messageService, IUpcomingEventService upcomingEventService)
        {
            _messageService = messageService;
            _upcomingEventService = upcomingEventService;
        }

        public async Task<bool> ContactUs(string name, string from, string subject, string text)
        {
            await _messageService.SendEmailAsync(name, from, subject, text);

            return true;
        }

        public async Task<bool> ScrapeUpcomingEvents()
        {
            await _upcomingEventService.ScrapeUpcomingEvents();

            return true;
        }
    }
}
