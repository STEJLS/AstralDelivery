/// <summary>
/// Сервис хэширования
/// </summary>
namespace AstralDelivery.Domain.Abstractions
{
    /// <summary>
    /// Сервис хэширования
    /// </summary>
    public interface IHashingService
    {
        /// <summary>
        /// Возвращает хэш в виде строки
        /// </summary>
        /// <param name="password"> Строка для хэширования </param>
        /// <returns></returns>
        string Get(string password);
    }
}
