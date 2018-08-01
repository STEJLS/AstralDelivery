using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralDelivery.Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Конструктор по умолчанию 
        /// </summary>
        public User()
        {

        }

        /// <summary>
        /// Конструтор с четырьмя параметрами string, string, string, Role
        /// </summary>
        /// <param name="login"> Логин </param>
        /// <param name="password"> Пароль </param>
        /// <param name="email"> Почта </param>
        /// <param name="role"> Роль </param>
        public User(string login, string password, string email, Role role)
        {
            UserGuid = Guid.NewGuid();
            Login = login;
            Password = password;
            Email = email;
            Role = role;
        }
    }
}
