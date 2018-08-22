using AstralDelivery.Domain.Entities;
using AstralDelivery.Models.Enums;
using System;

namespace AstralDelivery.Models.Search
{
    /// <summary>
    /// Модель для поиска товаров
    /// </summary>
    public class ProductSearchModel
    {
        /// <summary>
        /// Строка по которой ведется поиск
        /// </summary>
        public string SearchString { get; set; } = "";
        /// <summary>
        /// <see cref="ProductSortField"/>
        /// </summary>
        public ProductSortField ProductSortField { get; set; }
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
        /// <summary>
        /// Направление сортировки
        /// </summary>
        public bool Direction { get; set; } = true;
        /// <summary>
        /// Необходимое количество менеджеров
        /// </summary>
        public int Count { get; set; } = 10;
        /// <summary>
        /// Сколько менеджеров необходимо пропустить
        /// </summary>
        public int Offset { get; set; } = 0;
    }
}
