using System.Threading.Tasks;
using AstralDelivery.MailService.Abstractions;
using Microsoft.Extensions.Logging;

namespace AstralDelivery.MailService.Services
{
    /// <inheritdoc />
    class MailServiceStub : IMailService
    {
        private readonly ILogger _logger;

        public MailServiceStub(ILogger<MailServiceStub> logger)
        {
            _logger = logger;
        }
        /// <inheritdoc />
        public async Task SendAsync(string destination, string message, string subject)
        {
            await Task.Run(() =>
            {
                _logger.LogInformation("Сообщение направлялось -> " + destination);
                _logger.LogInformation("Тема сообщения:  " + subject);
                _logger.LogInformation("Текст сообщения:\n " + message);
            });
        }
    }
}
