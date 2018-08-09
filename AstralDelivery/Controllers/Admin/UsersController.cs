using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers.Admin
{
    /// <summary>
    /// Контроллер администратора управляющий менеджерами
    /// </summary>
    [Route("Admin")]
    [Authorize(Roles = nameof(Role.Admin))]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Создает менеджера
        /// </summary>
        /// <param name="model"> <see cref="ManagerModel"/> </param>
        /// <returns></returns>
        [HttpPost("User")]
        public new async Task<Guid> User([FromBody] UserInfo model)
        {
            return await _userService.Create(model);
        }

        /// <summary>
        /// Редактирует данные менеджера
        /// </summary>
        /// <param name="model"> <see cref="UserInfo"/> </param>
        /// <returns></returns>
        [HttpPut("User/{userGuid}")]
        public new async Task User([FromRoute] Guid userGuid, [FromBody] UserInfo model)
        {
            await _userService.AdminEdit(userGuid, model);
        }

        /// <summary>
        /// Удаляет менеджера
        /// </summary>
        /// <param name="model"> <see cref="Guid"/> </param>
        /// <returns></returns>
        [HttpDelete("User/{userGuid}")]
        public new async Task User([FromRoute] Guid userGuid)
        {
            await _userService.Delete(userGuid);
        }

        /// <summary>
        /// Поиск и сортировка менеджеров
        /// </summary>
        /// <returns></returns>
        [HttpGet("SearchManagers")]
        public ManagerSearchResult SearchManagers([FromQuery] SearchManagerModel model)
        {
            var managers = _userService.SearchManagers(model.SearchString);

            return new ManagerSearchResult(
                managers.Count(),
                UserSortManager.Sort(managers, model.Field, model.Direction, model.Count, model.Offset));
        }
    }
}
