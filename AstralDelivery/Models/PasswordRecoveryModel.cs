using System;

namespace AstralDelivery.Models
{
    /// <summary>
    /// Модель для восстановления пароля
    /// </summary>
    public class PasswordRecoveryModel
    {
        /// <summary>
        /// Токен
        /// </summary>
        public Guid Token{ get; set; }
        /// <summary>
        /// Новый пароль
        /// </summary>
        public string NewPassword { get; set; }
    }
}
