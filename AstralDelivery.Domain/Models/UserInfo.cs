using AstralDelivery.Domain.Entities;

namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Входная модель пользователя
    /// </summary>
    public class UserInfo
    {
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
    }
}