using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models.Product;
using System;
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
        /// Возвращает информацию о товаре
        /// </summary>
        /// <param name="productGuid"> Идентификатор </param>
        /// <returns></returns>
        Task<Product> Get(Guid productGuid);
    }
}
