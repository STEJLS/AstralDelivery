﻿using AstralDelivery.Domain.Entities;
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
        public string City { get; set; }
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
        /// Роль
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// Активированный ли аккаунт
        /// </summary>
        public bool IsActivated { get; set; }

        public UserModel()
        {

        }

        public UserModel(User user)
        {
            UserGuid = user.UserGuid;
            Email = user.Email;
            City = user.City;
            Surname = user.Surname;
            Name = user.Name;
            Patronymic = user.Patronymic;
            Role = user.Role;
            IsActivated = user.IsActivated;
        }
    }
}
