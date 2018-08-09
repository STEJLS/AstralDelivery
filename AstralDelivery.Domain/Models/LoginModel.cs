namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Модель для входа в систему
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Прекращать ли сессию при закрытии браузера
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
