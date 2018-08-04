using System;

namespace AstralDelivery.Domain.Models
{
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
