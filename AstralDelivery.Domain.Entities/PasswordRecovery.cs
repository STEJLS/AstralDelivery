using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralDelivery.Domain.Entities
{
    /// <summary>
    /// Класс для восстановления пароля
    /// </summary>
    public class PasswordRecovery
    {
        /// <summary>
        /// Уникальный токен
        /// </summary>
        [Key]
        public Guid Token { get; set; }
        /// <summary>
        /// Guid пользователя
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Дата и время создания токена восстановления
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        [ForeignKey("UserGuid")]
        public virtual User User { get; set; }

        /// <summary />
        public PasswordRecovery(Guid userGuid)
        {
            Token = new Guid();
            UserGuid = userGuid;
            CreationDate = DateTime.Now;
        }
    }

}
