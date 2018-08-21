using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Models.Product;
using AstralDelivery.Domain.Models.Search;
using AstralDelivery.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers
{
    [Authorize(Roles = nameof(Role.Сourier))]
    [Route("Courier")]
    public class CourierController : Controller
    {
        private readonly IProductService _productService;

        public CourierController(IProductService productService)
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
        [HttpPost]
        [Route("/SetProductStatus/{ProductGuid}")]
        public async Task SetProductStatus([FromRoute] Guid productGuid, [FromBody] SetStatusModel model)
        {
            await _productService.SetStatus(productGuid, model.DeliveryStatus);
        }

        /// <summary>
        /// Поиск, сортировка, фильтрация товара
        /// </summary>
        /// <param name="model"> <see cref="SearchProductModel"/> </param>
        /// <returns></returns>
        [HttpGet]
        [Route("/SearchProducts")]
        public SearchResult<ProductSearchInfoForCourier> Product([FromQuery] SearchProductModel model)
        {
            var products = _productService.Search(model.SearchString, model.DateFilter, model.DeliveryTypeFilter, null);

            return new SearchResult<ProductSearchInfoForCourier>(
                products.Count(),
                SortManager.SortProductsForCourier(products, model.ProductSortField, model.Direction, model.Count, model.Offset));
        }
    }
}
