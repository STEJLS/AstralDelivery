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
        /// Почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string DeliveryPointName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// Удален ли пользователь
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Активирован ли аккаунт
        /// </summary>
        public bool IsActivated { get; set; }
        /// <summary>   
        /// Дата создания
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Идентификатор пункта выдачи
        /// </summary>
        [ForeignKey(nameof(DeliveryPoint))]
        public Guid DeliveryPointGuid { get; set; }
        /// <summary>
        /// Пункт выдачи
        /// </summary>
        public virtual DeliveryPoint DeliveryPoint { get; set; }

        /// <summary>
        /// Конструктор по умолчанию 
        /// </summary>
        public User()
        {

        }

        /// <summary/>
        /// <param name="email"> Почта </param>
        /// <param name="password"> Пароль /param>
        /// <param name="role"> Роль </param>
        public User(string email, string password, Role role, Guid deliveryPointGuid)
        {
            UserGuid = Guid.NewGuid();
            Email = email;
            Password = password;
            Role = role;
            IsDeleted = false;
            IsActivated = false;
            DeliveryPointGuid = deliveryPointGuid;
            Date = DateTime.Now;
        }

        /// <summary/>
        /// <param name="email"> Почта </param>
        /// <param name="password"> Пароль </param>
        /// <param name="deliveryPointName"> Город </param>
        /// <param name="surname"> Фамилия </param>
        /// <param name="name"> Имя </param>
        /// <param name="patronymic"> Отчество </param>
        /// <param name="role"> Роль </param>
        public User(string email, string password, string deliveryPointName, string surname, string name, string patronymic, string phone, Role role, Guid deliveryPointGuid) : this(email, password, role, deliveryPointGuid)
        {
            DeliveryPointName = deliveryPointName;
            Surname = surname;
            Name = name;
            Patronymic = patronymic;
            Phone = phone;
        }
    }
}
