using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Abstractions
{
    /// <summary>
    /// Сервис по работе с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Соездает администратора
        /// </summary>
        /// <param name="password"> Пароль </param>
        /// <param name="email"> Почта </param>
        /// <returns></returns>
        Task<Guid> CreateAdmin(string email, string password);

        /// <summary>
        /// Соездает нового пользователя 
        /// </summary>
        /// <param name="model"> <see cref="UserInfo"/> </param>
        /// <returns></returns>
        Task<Guid> Create(UserInfo model);

        /// <summary>
        /// Редактирует данные пользователя(Для администратора).
        /// Для редактирования дуступны все поля кроме пароля.
        /// </summary>
        /// <param name="guid"> Идентификатор </param>
        /// <param name="model"> <see cref="UserInfo"/> </param>
        /// <returns></returns>
        Task AdminEdit(Guid guid, UserInfo model);

        /// <summary>
        /// Редактирует данные пользователя
        /// </summary>
        /// <param name="guid"> Идентификатор </param>
        /// <param name="model"> <see cref="UserInfo"/> </param>
        /// <returns></returns>
        Task Edit(Guid guid, UserInfo model);

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        /// <param name="UserGuid"> Идентификатор </param>
        /// <returns></returns>
        Task Delete(Guid UserGuid);

        /// <summary>
        /// Изменяет пароль пользователя
        /// </summary>
        /// <param name="model"> <see cref="ChangePasswordModel"/> </param>
        /// <returns></returns>
        Task ChangePassword(ChangePasswordModel model);

        /// <summary>
        /// Осуществляет поиск по строке
        /// </summary>
        /// <param name="searchString"> Строка для поиска </param>
        /// <returns></returns>
        IEnumerable<User> SearchManagers(string searchString);
    }
}
