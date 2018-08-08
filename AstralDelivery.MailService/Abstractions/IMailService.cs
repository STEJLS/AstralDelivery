using System.Threading.Tasks;

namespace AstralDelivery.MailService.Abstractions
{
    /// <summary>
    /// Сервис отправки сообщений на электронную почту
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Отправляет сообщение на электронную почту
        /// </summary>
        /// <param name="destination"> Электронный адрес получателя </param>
        /// <param name="message"> Сообщение </param>
        /// <param name="subject"> Тема </param>
        /// <returns></returns>
        Task SendAsync(string destination, string message, string subject);
    }
}
