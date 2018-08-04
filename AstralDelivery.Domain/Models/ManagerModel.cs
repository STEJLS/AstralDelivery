namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Модель для создания менеджера
    /// </summary>
    public class ManagerModel
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
    }
}