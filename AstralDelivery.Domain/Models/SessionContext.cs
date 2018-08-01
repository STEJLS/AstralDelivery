using System;
using AstralDelivery.Domain.Entities;
using Newtonsoft.Json;

namespace AstralDelivery.Domain.Models
{
    
    /// <summary>
    /// Контекст сессии
    /// </summary>
    public class SessionContext
    {
        /// <summary>
        /// Авторизованы ли мы
        /// </summary>
        [JsonProperty("isAuthorized")]
        public bool Authorized { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }

        /// <summary/>
        public SessionContext()
        {
            Authorized = false;
        }

        /// <summary/>
        public SessionContext(User user)
        {
            Authorized = true;
            UserGuid = user.UserGuid;
            Login = user.Login;
            Email = user.Email;
        }
    }
}
