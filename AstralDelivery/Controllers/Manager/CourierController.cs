using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Models.Search;
using AstralDelivery.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers.Manager
{
    [Authorize(Roles = nameof(Role.Manager))]
    [Route("Manager/Courier")]
    public class CourierController : Controller
    {
        private readonly IUserService _userService;

        public CourierController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<Guid> Courier([FromBody] UserInfo model)
        {
            return await _userService.CreateCourier(model);
        }

        [HttpPut("{CourierGuid}")]
        public async Task Courier([FromRoute]Guid courierGuid, [FromBody] UserInfo model)
        {
            await _userService.Edit(courierGuid, model);
        }

        [HttpDelete("{CourierGuid}")]
        public async Task Courier([FromRoute]Guid courierGuid)
        {
            await _userService.DeleteCourier(courierGuid);
        }

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
        public async Task<SearchResult<UserModel>> Courier([FromQuery] SearchManagerModel model)
        {
            var managers = await _userService.SearchCouriers(model.SearchString);

            return new SearchResult<UserModel>(
                managers.Count(),
                SortManager.SortManagers(managers, model.Field, model.Direction, model.Count, model.Offset));
        }
    }
}
