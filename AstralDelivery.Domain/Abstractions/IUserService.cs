using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Abstractions
{
    /// <summary>
    /// Сервис создания новых пользователей
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Соездает администратора
        /// </summary>
        /// <param name="password"> Пароль </param>
        /// <param name="email"> Почта </param>
        /// <returns></returns>
        Task Create(string email, string password);

        /// <summary>
        /// Соездает нового пользователя 
        /// </summary>
        /// <param name="email"> Почта </param>
        /// <param name="city"> Город </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="name"> Имя </param>
        /// <param name="patronymic"> Отчетсво </param>
        /// <param name="role"> Роль </param>
        /// <returns></returns>
        Task Create(string email, string city, string surname, string name, string patronymic, Role role);

        /// <summary>
        /// Возвращает всех менеджеров
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserModel> GetManagers();

        /// <summary>
        /// Редактирует данные пользователя
        /// </summary>
        /// <param name="userModel"> <see cref="UserModel"/> </param>
        /// <returns></returns>
        Task Edit(UserModel userModel);

        /// <summary>
        /// Редактирует данные пользователя
        /// </summary>
        /// <param name="guid"> Идентификатор </param>
        /// <param name="email"> Почта </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="name"> Имя </param>
        /// <param name="patronymic"> Отчество </param>
        /// <returns></returns>
        Task Edit(Guid guid, string email, string surname, string name, string patronymic);

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
    }
}
