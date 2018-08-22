using AstralDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstralDelivery.Models.DeliveryPoint
{
    /// <summary>
    /// Выходная, полная модель пункта выдачи
    /// </summary>
    public class DeliveryPointFullInfo
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
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
        /// Телефон
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// <see cref="WorkTime"/>
        /// </summary>
        public List<WorkTime> WorksSchedule { get; set; }
        /// <summary>
        /// Менеджеры, относящиеся к данному пункту выдачи
        /// </summary>
        public List<UserModel> Managers { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="point"> <see cref="Entities.DeliveryPoint "/> </param>
        public DeliveryPointFullInfo(Domain.Entities.DeliveryPoint point)
        {
            Guid = point.Guid;
            Name = point.Name;
            City = point.City;
            Street = point.Street;
            Building = point.Building;
            Corpus = point.Corpus;
            Office = point.Office;
            Phone = point.Phone;
            Date = point.Date;
            WorksSchedule = point.WorksSchedule;
            Managers = point.Managers.Select(u => new UserModel(u)).ToList();
        }
    }
}
