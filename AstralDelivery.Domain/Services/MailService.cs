using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;

namespace AstralDelivery.Domain.Services
{
    public class MailService : IMailService
    {
        private readonly ConfigurationOptions _options;

        public MailService(ConfigurationOptions options)
        {
            _options = options;
        }

        public async Task Send(string message, string subject)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", _options.ServiceEmail));
            emailMessage.To.Add(new MailboxAddress("", _options.AdminEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 25, false);
                await client.AuthenticateAsync(_options.ServiceEmail, _options.ServicePassword);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
