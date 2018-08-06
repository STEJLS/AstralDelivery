using System;
using System.Collections.Generic;
using System.Text;

namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Модель для редактирования пароля
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// Старый пароль
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// Новый пароль
        /// </summary>
        public string NewPassword { get; set; }
    }
}
