using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Models.Search;
using AstralDelivery.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public CourierController(IUserService userService)
        {
            _userService = userService;
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
            return await _userService.GetUserInfo(courierGuid);
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
    }
}
