using AstralDelivery.Domain.Entities;
using System.Collections.Generic;

namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Входная модель для пункта выдачи
    /// </summary>
    public class DeliveryPointModel
    {
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
        /// <see cref="WorkTime"/>
        /// </summary>
        public ICollection<WorkTime> WorksSchedule { get; set; }
    }
}