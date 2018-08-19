using AstralDelivery.Domain.Entities;
using System;

namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Выходная модель пользователя
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string DeliveryPointName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// Активированный ли аккаунт
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>
        /// Идентификатор пункта выдачи
        /// /// </summary>
        public Guid DeliveryPointGuid { get; set; }

        public UserModel()
        {

        }

        public UserModel(User user)
        {
            UserGuid = user.UserGuid;
            Email = user.Email;
            DeliveryPointName = user.DeliveryPointName;
            Surname = user.Surname;
            Name = user.Name;
            Patronymic = user.Patronymic;
            Phone = user.Phone;
            Role = user.Role;
            IsActivated = user.IsActivated;
            DeliveryPointGuid = user.DeliveryPointGuid;
        }
    }
}

