using System.Threading.Tasks;

namespace AstralDelivery.Domain.Abstractions
{
    /// <summary>
    /// Сервис отправки сообщений на электронную почту
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Отправляет сообщение на электронную почту
        /// </summary>
        /// <param name="message"> Текст сообщения </param>
        /// <param name="subject"> Тема сообщения </param>
        /// <returns></returns>
        Task Send(string message, string subject);
    }
}
