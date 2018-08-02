namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Параметры конфигурации
    /// </summary>
    public class ConfigurationOptions
    {
        /// <summary>
        /// Соль хеширования паролей
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// Почта приложения
        /// </summary>
        public string ServiceEmail { get; set; }
        /// <summary>
        /// Пароль от почты приложения
        /// </summary>
        public string ServicePassword { get; set; }
        /// <summary>
        /// Почта администратора
        /// </summary>
        public string AdminEmail { get; set; }
        /// <summary>
        /// Пароль администратора
        /// </summary>
        public string AdminPassword { get; set; }
    }
}
