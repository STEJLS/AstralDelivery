using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Abstractions
{
    /// <summary>
    /// Сервис для работы с товарами
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Создает товар
        /// </summary>
        /// <param name="productInfo"> <see cref="ProductInfo"/> </param>
        /// <returns></returns>
        Task<Guid> Create(ProductInfo productInfo);

        /// <summary>
        /// Возвращает информацию о товаре для курьера
        /// </summary>
        /// <param name="productGuid"> Идентификатор </param>
        /// <returns></returns>
        Task<Product> GetProductForCourier(Guid productGuid);

        /// <summary>
        /// Устанавливает статус товара 
        /// </summary>
        /// <param name="productGuid"> Идентификатор товара </param>
        /// <param name="deliveryStatus"> Новый статус </param>
        /// <returns></returns>
        Task SetStatus(Guid productGuid, DeliveryStatus deliveryStatus);

        /// <summary>
        /// Редактирует товар
        /// </summary>
        /// <param name="productGuid"> Идентификатор </param>
        /// <param name="productInfo"> <see cref="ProductInfo"/> </param>
        /// <returns></returns>
        Task Edit(Guid productGuid, ProductInfo productInfo);

        /// <summary>
        /// Удаляет товар
        /// </summary>
        /// <param name="productGuid"> Идентификатор </param>
        /// <returns></returns>
        Task Delete(Guid productGuid);

        /// <summary>
        /// Возвращает информацию о товаре для менеджера
        /// </summary>
        /// <param name="productGuid"> Идентификатор </param>
        /// <returns></returns>
        Task<Product> GetProductForManager(Guid productGuid);

        /// <summary>
        /// Осуществляет поиск товаров
        /// </summary>
        /// <param name="searchString"> Строка для поиска </param>
        /// <param name="dateFilter"> Дата доставки </param>
        /// <param name="deliveryTypeFilter"> Тип доставки </param>
        /// <param name="deliveryStatusFilter"> Статус доставки </param>
        /// <param name="courierGuid"> Идентификатор курьера </param>
        /// <returns></returns>
        IEnumerable<Product> Search(string searchString, DateTime? dateFilter, DeliveryType? deliveryTypeFilter, DeliveryStatus? deliveryStatusFilter, Guid? courierGuid);

        /// <summary>
        /// Назначает курьера
        /// </summary>
        /// <param name="productGuid"> Идентификатор товара </param>
        /// <param name="courierGuid"> Идентификатор курьера </param>
        /// <returns></returns>
        Task SetCourier(Guid productGuid, Guid courierGuid);
    }
}
