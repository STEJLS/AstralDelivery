using AstralDelivery.Domain.Entities;
using System.Threading.Tasks;


namespace AstralDelivery.Domain.Abstractions
{
    /// <summary>
    /// Сервис создания новых пользователей
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Соездает нового пользователя 
        /// </summary>
        /// <param name="login"> Логин </param>
        /// <param name="password"> Пароль </param>
        /// <param name="email"> Почта </param>
        /// <param name="role"> Роль </param>
        /// <returns></returns>
        Task Create(string email, string password, Role role);
    }
}
