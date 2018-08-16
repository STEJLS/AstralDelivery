using AstralDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Выходная модель пункта выдачи
    /// </summary>
    public class DeliveryPointModel
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Guid{ get; set; }
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
        public string Address { get; set; }
        /// <summary>
        /// Количество менеджеров
        /// </summary>
        public int CountOfManagers { get; set; }
        /// <summary>
        /// <see cref="WorkTime"/>
        /// </summary>
        public ICollection<WorkTime> WorksSchedule { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Date { get; set; }

        public DeliveryPointModel(DeliveryPoint point)
        {
            Guid = point.Guid;
            Name = point.Name;
            City = point.City;
            Address = $"улица {point.Street} дом {point.Building}{point.Corpus}";
            if (point.Office != 0)
            {
                Address += $" офис {point.Office}";
            }
            CountOfManagers = point.Managers.Count;
            WorksSchedule = point.WorksSchedule;
            Date = point.Date;
        }
    }
}
