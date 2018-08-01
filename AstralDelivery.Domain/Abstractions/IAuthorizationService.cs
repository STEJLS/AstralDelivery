using AstralDelivery.Domain.Entities;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Abstractions
{
    /// <summary>
    /// Сервис авторизации пользователей
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        Task Login(string login, string password, bool rememberMe);

        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <returns></returns>
        Task Logout();
    }
}
