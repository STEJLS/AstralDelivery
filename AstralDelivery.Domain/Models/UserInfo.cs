using AstralDelivery.Domain.Entities;
using System;

namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Входная модель пользователя
    /// </summary>
    public class UserInfo
    {
        /// <summary>в
        /// Почта
        /// </summary>
        public string Email { get; set; }
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
        /// Идентификатор пункта выдача
        /// </summary>
        public Guid DeliveryPointGuid { get; set; }
    }
}