using graphQLService.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace graphQLService.Services
{
    /// <summary>
    /// Todo: could be but in a seperate rest api (minimal api)
    /// </summary>
    public class MessageService : IMessageService
    {
        private string _email;
        private string _password;

        public MessageService(IConfiguration configuration)
        {
            _email = configuration.GetConnectionString("email");
            _password = configuration.GetConnectionString("password");
        }

        public async Task SendEmailAsync(string fromDisplayName, string fromEmailAddress, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromDisplayName, fromEmailAddress));
            email.To.Add(new MailboxAddress("Support", _email));
            email.Subject = subject;

            var body = new BodyBuilder
            {
                HtmlBody = "Clients name: " + fromDisplayName + "<hr>" +
                "From: " + fromEmailAddress + "<hr>" +
                "Content: " + message
            };

            email.Body = body.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                // Start of provider specific settings
                await client.ConnectAsync("smtp.gmail.com", 587, false).ConfigureAwait(false);
                await client.AuthenticateAsync(_email, _password).ConfigureAwait(false);
                // End of provider specific settings
                await client.SendAsync(email).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
