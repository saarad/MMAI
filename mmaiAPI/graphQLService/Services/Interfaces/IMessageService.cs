using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using graphQLService.Services.Interfaces.DependencyInjection;

namespace graphQLService.Services.Interfaces
{
    public interface IMessageService : ITransient
    {
        Task SendEmailAsync(
            string fromDisplayName,
            string fromEmailAddress,
            string subject,
            string message);
    }
}
