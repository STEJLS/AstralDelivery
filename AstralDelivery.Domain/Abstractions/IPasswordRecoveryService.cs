using System.Threading.Tasks;
using System;

namespace AstralDelivery.Domain.Abstractions
{
    /// <summary>
    /// Cервис восстановления пароля
    /// </summary>
    public interface IPasswordRecoveryService
    {
        /// <summary>
        /// Создает уникальный токен для восстановления пароля
        /// </summary>
        /// <param name="email"> Почта </param>
        /// <param name="host"> Хост </param>
        /// <returns></returns>
        Task CreateToken(string email, string host);

        /// <summary>
        /// Изменяет пароль
        /// </summary>
        /// <param name="token"> Уникальный токен </param>
        /// <param name="password"> Новый пароль </param>
        /// <returns></returns>
        Task ChangePassword(Guid token, string password);

        /// <summary>
        /// Проверяет действительный ли токен
        /// </summary>
        /// <param name="token"> Токен </param>
        /// <returns></returns>
        Task<bool> CheckToken(Guid token);
    }
}
