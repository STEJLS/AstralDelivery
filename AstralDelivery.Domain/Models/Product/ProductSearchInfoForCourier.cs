using AstralDelivery.Domain.Entities;
using System;

namespace AstralDelivery.Domain.Models.Product
{
    /// <summary>
    /// Выходная модель для поиска товаров для курьера
    /// </summary>
    public class ProductSearchInfoForCourier
    {
        /// <summary>
        /// Артикул
        /// </summary>
        public string Article { get; set; }
        /// <summary>
        /// Наименования
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Дата и время доставки
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Статус доставки
        /// </summary>
        public DeliveryStatus DeliveryStatus { get; set; }

        public ProductSearchInfoForCourier(Entities.Product product)
        {
            Article = product.Article;
            Name = product.Name;
            DateTime = product.DateTime;
            Address = product.Address;
            DeliveryStatus = product.DeliveryStatus;
        }
    }
}
