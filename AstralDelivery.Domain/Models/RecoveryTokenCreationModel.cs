using System;
using System.Collections.Generic;
using System.Text;

namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Модель для создания токена восстановления
    /// </summary>
    public class RecoveryTokenCreationModel
    {
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
    }
}
