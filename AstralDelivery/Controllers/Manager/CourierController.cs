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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers.Manager
{
    /// <summary>
    /// Контроллер манеджера управляющий курьерами
    /// </summary>
    [Authorize(Roles = nameof(Role.Manager))]
    [Route("Manager/Courier")]
    public class CourierController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public CourierController(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }

        /// <summary>
        /// Создает курьера
        /// </summary>
        /// <param name="model"> <see cref="UserInfo"/> </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> CreateCourier([FromBody] UserInfo model)
        {
            return await _userService.CreateCourier(model);
        }

        /// <summary>
        /// Редактирует курьера
        /// </summary>
        /// <param name="courierGuid"> Идентификатор </param>
        /// <param name="model"> <see cref="UserInfo"/> </param>
        /// <returns></returns>
        [HttpPut("{CourierGuid}")]
        public async Task EditCourier([FromRoute]Guid courierGuid, [FromBody] UserInfo model)
        {
            await _userService.Edit(courierGuid, model);
        }

        /// <summary>
        /// Удаляет курьера
        /// </summary>
        /// <param name="courierGuid"> Идентификатор </param>
        /// <returns></returns>
        [HttpDelete("{CourierGuid}")]
        public async Task DeleteCourier([FromRoute]Guid courierGuid)
        {
            await _userService.DeleteCourier(courierGuid);
        }

        /// <summary>
        /// Возвращает курьера
        /// </summary>
        /// <param name="courierGuid"> Идентификатор </param>
        /// <returns></returns>
        [HttpGet("{CourierGuid}")]
        public async Task<UserModel> GetCourier([FromRoute]Guid courierGuid)
        {
            return new UserModel(await _userService.GetUserInfo(courierGuid));
        }

        /// <summary>
        /// Поиск и сортировка курьеров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<SearchResult<UserModel>> SearchCourier([FromQuery] ManagerSearchModel model)
        {
            var managers = await _userService.SearchCouriers(model.SearchString);

            return new SearchResult<UserModel>(
                managers.Count(),
                SortManager.SortManagers(managers, model.Field, model.Direction, model.Count, model.Offset));
        }

        /// <summary>
        /// Возвращает курьеров у которых меньше 6 заказов в конкретный день
        /// </summary>
        /// <param name="dateTime"> День на который осуществляется запрос </param>
        /// <returns></returns>
        [HttpGet("GetFree")]
        public async Task<List<UserModel>> GetFreeCourier([FromQuery] DateTime dateTime)
        {
            return (await _userService.GetFreeCourier(dateTime)).Select(u => new UserModel(u)).ToList();
        }

        /// <summary>
        /// Возвращает, сортирует доставленные товары курьера
        /// </summary>
        /// <param name="courierGuid"> идентификатор курьера </param>
        /// <param name="model"> <see cref="ProductSearchModel"/> </param>
        /// <returns></returns>
        [HttpGet("GetDeliveryHistory/{CourierGuid}")]
        public SearchResult<ProductSearchInfoForManager> GetDeliveryHistory([FromRoute] Guid courierGuid, [FromQuery] ProductSearchModel model)
        {
            var products = _productService.Search(model.SearchString, model.DateFilter, DeliveryType.Courier, DeliveryStatus.Delivered, courierGuid);

            return new SearchResult<ProductSearchInfoForManager>(
                products.Count(),
                SortManager.SortProductsForManager(products, model.ProductSortField, model.Direction, model.Count, model.Offset).Select(p => new ProductSearchInfoForManager(p))
                );
        }

    }
}
