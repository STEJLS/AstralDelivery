using AstralDelivery.Domain.Entities;
using System;

namespace AstralDelivery.Models
{
    /// <summary>
    /// Модель для отдачи XML файла
    /// </summary>
    public class XMLFilterModel
    {
        /// <summary>
        /// Фильтрация по дате доставки
        /// </summary>
        public DateTime? DateFilter { get; set; }
        /// <summary>
        /// Фильтрация по типу доставки
        /// </summary>
        public DeliveryType? DeliveryTypeFilter { get; set; }
        /// <summary>
        /// Фильтрация по статусу доставки
        /// </summary>
        public DeliveryStatus? DeliveryStatusFilter { get; set; }
    }
}
