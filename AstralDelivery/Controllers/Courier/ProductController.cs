using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Models;
using AstralDelivery.Models.Product;
using AstralDelivery.Models.Search;
using AstralDelivery.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers.Courier
{
    /// <summary>
    /// Контроллер курьера для управления товарами
    /// </summary>
    [Authorize(Roles = nameof(Role.Сourier))]
    [Route("Courier")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Возвращает информацию о товаре
        /// </summary>
        /// <param name="productGuid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/GetProduct/{ProductGuid}")]
        public async Task<Product> GetProduct([FromRoute] Guid productGuid)
        {
            return await _productService.GetProductForCourier(productGuid);
        }

        /// <summary>
        /// Устанавливет статус доставки товара
        /// </summary>
        /// <param name="productGuid"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/SetProductStatus/{ProductGuid}")]
        public async Task SetProductStatus([FromRoute] Guid productGuid, [FromBody] SetStatusModel model)
        {
            await _productService.SetStatus(productGuid, model.DeliveryStatus);
        }

        /// <summary>
        /// Поиск, сортировка, фильтрация товара
        /// </summary>
        /// <param name="model"> <see cref="ProductSearchModel"/> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("/SearchProducts")]
        public SearchResult<ProductSearchInfoForCourier> SearchProduct([FromQuery] ProductSearchModel model)
        {
            var products = _productService.Search(model.SearchString, model.DateFilter, model.DeliveryTypeFilter, null, null);

            return new SearchResult<ProductSearchInfoForCourier>(
                products.Count(),
                SortManager.SortProductsForCourier(products, model.ProductSortField, model.Direction, model.Count, model.Offset));
        }
    }
}
