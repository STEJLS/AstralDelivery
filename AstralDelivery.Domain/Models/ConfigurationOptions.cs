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
    }
}
