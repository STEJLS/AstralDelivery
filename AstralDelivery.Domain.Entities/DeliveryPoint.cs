﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralDelivery.Domain.Entities
{
    /// <summary>
    /// Пунк выдачи
    /// </summary>
    public class DeliveryPoint
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Guid { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Дом
        /// </summary>
        public int Building { get; set; }
        /// <summary>
        /// Корпус
        /// </summary>
        public string Corpus { get; set; }
        /// <summary>
        /// Офис
        /// </summary>
        public int Office { get; set; }
        /// <summary>
        /// Удален ли пользователь
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// <see cref="WorkTime"/>
        /// </summary>
        public virtual List<WorkTime> WorksSchedule { get; set; }
        /// <summary>
        /// Менеджеры, относящиеся к данному пункту выдачи
        /// </summary>
        public virtual List<User> Managers { get; set; }

        public DeliveryPoint()
        {

        }

        /// <summary/>
        /// <param name="name"> Название </param>
        /// <param name="city"> Город </param>
        /// <param name="street"> Улица </param>
        /// <param name="building"> Дом </param>
        /// <param name="corpus"> Корпус </param>
        /// <param name="office"> Офис </param>
        public DeliveryPoint(string name, string city, string street, int building, string corpus, int office)
        {
            Guid = Guid.NewGuid();
            Name = name;
            City = city;
            Street = street;
            Building = building;
            Corpus = corpus;
            Office = office;
            IsDeleted = false;
            Date = DateTime.Now;
        }
    }
}
