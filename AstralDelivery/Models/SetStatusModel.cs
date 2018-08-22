using AstralDelivery.Domain.Entities;

namespace AstralDelivery.Models
{
    /// <summary>
    /// Модель для установки статуса доставки товару
    /// </summary>
    public class SetStatusModel
    {
        /// <summary>
        /// <see cref="DeliveryStatus"/>
        /// </summary>
        public DeliveryStatus DeliveryStatus { get; set; }
    }
}
