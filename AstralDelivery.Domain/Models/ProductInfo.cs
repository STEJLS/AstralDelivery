using AstralDelivery.Domain.Entities;
using System;

namespace AstralDelivery.Domain.Models
{
    /// <summary>
    /// Входная модель товара
    /// </summary>
    public class ProductInfo
    {
        /// <summary>
        /// Артикул
        /// </summary>
        public string Article { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Номер телефона клиента
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Почта клиента
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Тип оплаты
        /// </summary>
        public PaymentType PaymentType { get; set; }
        /// <summary>
        /// Тип доставки
        /// </summary>
        public DeliveryType DeliveryType { get; set; }
        /// <summary>
        /// Дата и время доставки
        /// </summary>
        public DateTime DateTime { get; set; }
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
        public string House { get; set; }
        /// <summary>
        /// Корпус
        /// </summary>
        public string Corpus { get; set; }
        /// <summary>
        /// Квартира
        /// </summary>
        public int Flat { get; set; }
    }
}
