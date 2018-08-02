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
        /// <param name="email"> Почта </param>
        /// <param name="password"> Пароль </param>
        /// <param name="rememberMe"> Сохранять ли авторизацию после закрытия браузера </param>
        /// <returns></returns>
        Task<User> Login(string email, string password, bool rememberMe);

        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <returns></returns>
        Task Logout();
    }
}
