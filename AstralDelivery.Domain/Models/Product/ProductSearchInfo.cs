using AstralDelivery.Domain.Entities;
using System;

namespace AstralDelivery.Domain.Models.Product
{
    /// <summary>
    /// Выходная модель для поиска товаров 
    /// </summary>
    public class ProductSearchInfo
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
        /// Тип доставки
        /// </summary>
        public DeliveryType DeliveryType { get; set; }
        /// <summary>
        /// Статус доставки
        /// </summary>
        public DeliveryStatus DeliveryStatus { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="product"> <see cref="Product"/> </param>
        public ProductSearchInfo(Entities.Product product)
        {
            Article = product.Article;
            Name = product.Name;
            DateTime = product.DateTime;
            DeliveryType = product.DeliveryType;
            DeliveryStatus = product.DeliveryStatus;
        }
    }
}
