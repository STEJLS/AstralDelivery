using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Utils;
using AstralDelivery.Domain.Models.Search;
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
    [Route("Admin/Manager")]
    [Authorize(Roles = nameof(Role.Admin))]
    public class ManagerController : Controller
    {
        private readonly IUserService _userService;

        public ManagerController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Создает менеджера
        /// </summary>
        /// <param name="model"> <see cref="ManagerModel"/> </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Guid> Manager([FromBody] UserInfo model)
        {
            return await _userService.Create(model);
        }

        /// <summary>
        /// Редактирует данные менеджера
        /// </summary>
        /// <param name="model"> <see cref="UserInfo"/> </param>
        /// <returns></returns>
        [HttpPut("{managerGuid}")]
        public async Task Manager([FromRoute] Guid managerGuid, [FromBody] UserInfo model)
        {
            await _userService.AdminEdit(managerGuid, model);
        }

        /// <summary>
        /// Удаляет менеджера
        /// </summary>
        /// <param name="userGuid"> <see cref="Guid"/> </param>
        /// <returns></returns>
        [HttpDelete("{userGuid}")]
        public async Task Manager([FromRoute] Guid userGuid)
        {
            await _userService.DeleteManager(userGuid);
        }

        /// <summary>
        /// Поиск и сортировка менеджеров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public SearchResult<UserModel> Manager([FromQuery] SearchManagerModel model)
        {
            var managers = _userService.SearchManagers(model.SearchString);

            return new SearchResult<UserModel>(
                managers.Count(),
                SortManager.SortManagers(managers, model.Field, model.Direction, model.Count, model.Offset));
        }
    }
}
