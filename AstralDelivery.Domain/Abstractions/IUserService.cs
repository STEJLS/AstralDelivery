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
        Task<Guid> CreateAdmin(string email, string password, Guid deliveryPointGuid);

        /// <summary>
        /// Создает нового курьера
        /// </summary>
        /// <param name="model"> <see cref="UserInfo"/> </param>
        /// <returns></returns>
        Task<Guid> CreateCourier(UserInfo model);

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
        /// Возвращает информацию о пользователе
        /// </summary>
        /// <param name="guid"> <see cref="Guid"/> </param>
        /// <returns></returns>
        Task<User> GetUserInfo(Guid guid);

        /// <summary>
        /// Удаляет менеджера
        /// </summary>
        /// <param name="UserGuid"> Идентификатор </param>
        /// <returns></returns>
        Task DeleteManager(Guid UserGuid);

        /// <summary>
        /// Удаляет курьера
        /// </summary>
        /// <param name="UserGuid"> Идентификатор </param>
        /// <returns></returns>
        Task DeleteCourier(Guid UserGuid);

        /// <summary>
        /// Изменяет пароль пользователя
        /// </summary>
        /// <param name="oldPassword"> Старый пароль </param>
        /// <param name="newPassword"> Новый пароль </param>
        /// <returns></returns>
        Task ChangePassword(string oldPassword, string newPassword);

        /// <summary>
        /// Осуществляет поиск менеджеров
        /// </summary>
        /// <param name="searchString"> Строка для поиска </param>
        /// <returns></returns>
        IEnumerable<User> SearchManagers(string searchString);

        /// <summary>
        /// Осуществляет поиск курьеров
        /// </summary>
        /// <param name="searchString"> Строка для поиска </param>
        /// <returns></returns>
        Task<IEnumerable<User>> SearchCouriers(string searchString);

        /// <summary>
        /// Возвращает курьеров у которых меньше 6 заказов в конкретный день
        /// </summary>
        /// <param name="dateTime"> День на который осуществляется запрос </param>
        /// <returns></returns>
        Task<List<User>> GetFreeCourier(DateTime dateTime);
    }
}
