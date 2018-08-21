using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstralDelivery.Domain.Entities
{
    public class Product
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public Guid Guid { get; set; }
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
        /// Тип доставки
        /// </summary>
        public DeliveryType DeliveryType { get; set; }
        /// <summary>
        /// Тип оплаты
        /// </summary>
        public PaymentType PaymentType { get; set; }
        /// <summary>
        /// Статус товара
        /// </summary>
        public DeliveryStatus DeliveryStatus { get; set; }
        /// <summary>
        /// Дата и время добавления товара
        /// </summary>
        public DateTime CreationDate { get; set; }
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
        /// <summary>
        /// Идентификатор пунта выдачи
        /// </summary>
        [ForeignKey(nameof(DeliveryPoint))]
        public Guid DeliveryPointGuid { get; set; }
        /// <summary>
        /// Пункт выдачи
        /// </summary>
        public virtual DeliveryPoint DeliveryPoint { get; set; }
        /// <summary>
        /// Идентификатор курьера
        /// </summary>
        [ForeignKey("Courier")]
        public Guid? CourierGuid { get; set; }
        /// <summary>
        /// Курьер
        /// </summary>
        public virtual User Courier { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="article"> Артикул </param>
        /// <param name="name"> Наименование </param>
        /// <param name="count"> Количество </param>
        /// <param name="phone"> Телефон </param>
        /// <param name="email"> Почта </param>
        /// <param name="price"> Цена </param>
        /// <param name="paymentType"> Тип доставки </param>
        /// <param name="deliveryPointGuid"> Идентификатор пункта выдачи </param>
        public Product(string article, string name, int count, string phone, string email, int price, PaymentType paymentType, Guid deliveryPointGuid)
        {
            Guid = Guid.NewGuid();
            Article = article;
            Name = name;
            Count = count;
            Phone = phone;
            Email = email;
            Price = price;
            PaymentType = paymentType;
            DeliveryType = DeliveryType.Pickup;
            DeliveryPointGuid = deliveryPointGuid;
            DeliveryStatus = DeliveryStatus.ArrivedFromWarehouse;
            CreationDate = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="article"> Артикул </param>
        /// <param name="name"> Наименование </param>
        /// <param name="count"> Количество </param>
        /// <param name="phone"> Телефон </param>
        /// <param name="email"> Почта </param>
        /// <param name="price"> Цена </param>
        /// <param name="paymentType"> Тип доставки </param>
        /// <param name="deliveryPointGuid"> Идентификатор пункта выдачи </param>
        /// <param name="dateTime"> Дата и время доставки </param>
        /// <param name="city"> Город </param>
        /// <param name="street"> Улица </param>
        /// <param name="house"> Дом </param>
        /// <param name="corpus"> Корпус </param>
        /// <param name="flat"> Квартира </param>
        public Product(string article, string name, int count, string phone, string email, int price, PaymentType paymentType, Guid deliveryPointGuid, DateTime dateTime, string city, string street, string house, string corpus, int flat)
            : this(article, name, count, phone, email, price, paymentType, deliveryPointGuid)
        {
            DeliveryType = DeliveryType.Courier;
            DateTime = dateTime;
            City = city;
            Street = street;
            House = house;
            Corpus = corpus;
            Flat = flat;
        }

        /// <summary>
        /// Возвращает адрес доставки
        /// </summary>
        public string Address => $"Город {City} улица {Street} дом {House}{Corpus} квартира {Flat}";
    }
}
