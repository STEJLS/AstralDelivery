using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace AstralDelivery.MailService
{
    /// <summary>
    /// Сервис для отправки сообщений на электронную почту
    /// </summary>
    public class MailSender
    {
        /// <summary>
        /// Конфиг
        /// </summary>
        private readonly ConfigurationOptions _options;

        /// <summary>
        /// Конструктор без параметров, который устанавливает объект конфигурации
        /// </summary>
        public MailSender()
        {
            _options = new ConfigurationOptions();
        }

        /// <summary>
        /// Отправляет сообщение на электронную почту
        /// </summary>
        /// <param name="destination"> Электронный адрес получателя </param>
        /// <param name="message"> Сообщение </param>
        /// <param name="subject"> Тема </param>
        /// <returns></returns>
        public async Task SendAsync(string destination, string message, string subject)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_options.ServiceName, _options.ServiceEmail));
            emailMessage.To.Add(new MailboxAddress(string.Empty, destination));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_options.SMTPHost, _options.SMTPPort, _options.SMTPUseSSL);
                await client.AuthenticateAsync(new NetworkCredential(_options.ServiceEmail, _options.ServicePassword));
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
