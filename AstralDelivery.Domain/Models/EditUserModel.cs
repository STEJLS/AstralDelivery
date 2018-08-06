using AstralDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AstralDelivery.Domain.Models
{
    public class EditUserModel
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
    }
}
