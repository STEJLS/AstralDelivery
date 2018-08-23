using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using AstralDelivery.Models;
using AstralDelivery.Models.Product;
using AstralDelivery.Models.Search;
using AstralDelivery.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers.Manager
{
    /// <summary>
    /// Контроллер манеджера управляющий товарами
    /// </summary>
    [Authorize(Roles = nameof(Role.Manager))]
    [Route("Manager/Product")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Создает товар
        /// </summary>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> CreateProduct([FromBody] ProductInfo productInfo)
        {
            return await _productService.Create(productInfo);
        }

        /// <summary>
        /// Редактирует товар
        /// </summary>
        /// <param name="productGuid"> Идентификатор </param>
        /// <param name="productInfo"> <see cref="ProductInfo"/></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{productGuid}")]
        public async Task EditProduct([FromRoute] Guid productGuid, [FromBody] ProductInfo productInfo)
        {
            await _productService.Edit(productGuid, productInfo);
        }

        /// <summary>
        /// Удаляет товар
        /// </summary>
        /// <param name="productGuid"> Идентификатор </param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{productGuid}")]
        public async Task DeleteProduct([FromRoute] Guid productGuid)
        {
            await _productService.Delete(productGuid);
        }

        /// <summary>
        /// Возвращает товар
        /// </summary>
        /// <param name="productGuid"> Идентификатор </param>
        /// <returns></returns>
        [HttpGet]
        [Route("{productGuid}")]
        public async Task<Product> GetProduct([FromRoute] Guid productGuid)
        {
            return await _productService.GetProductForManager(productGuid);
        }

        /// <summary>
        /// Поиск, сортировка, фильтрация товара
        /// </summary>
        /// <param name="model"> <see cref="ProductSearchModel"/> </param>
        /// <returns></returns>
        [HttpGet]
        public SearchResult<ProductSearchInfoForManager> SearchProduct([FromQuery] ProductSearchModel model)
        {
            var products = _productService.Search(model.SearchString, model.DateFilter, model.DeliveryTypeFilter, model.DeliveryStatusFilter, null);

            return new SearchResult<ProductSearchInfoForManager>(
                products.Count(),
                SortManager.SortProductsForManager(products, model.ProductSortField, model.Direction, model.Count, model.Offset).Select(p => new ProductSearchInfoForManager(p))
                );
        }

        /// <summary>
        /// Назначает курьера
        /// </summary>
        /// <param name="productGuid"></param>
        /// <param name="courierGuid"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("SetCourier/{productGuid}")]
        public async Task SetCourier([FromRoute] Guid productGuid, [FromBody] CourierInfoModel model)
        {
            await _productService.SetCourier(productGuid, model.Guid);
        }
    }
}
