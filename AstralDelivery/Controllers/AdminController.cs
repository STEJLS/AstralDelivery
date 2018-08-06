using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System;
using AstralDelivery.Domain.Utils;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер управляющий деятельностью админа
    /// </summary>
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Создает менеджера
        /// </summary>
        /// <param name="model"> <see cref="ManagerModel"/> </param>
        /// <returns></returns>
        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPost("CreateManager")]
        public async Task CreateManager([FromBody] ManagerModel model)
        {
            await _userService.Create(model.Email, model.City, model.Surname, model.Name, model.Patronymic, Role.Manager);
        }

        /// <summary>
        /// Редактирует данные пользователя
        /// </summary>
        /// <param name="model"> <see cref="UserModel"/> </param>
        /// <returns></returns>
        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPut("EditUser")]
        public async Task EditUser([FromBody] UserModel model)
        {
            await _userService.Edit(model);
        }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        /// <param name="model"> <see cref="Guid"/> </param>
        /// <returns></returns>
        [Authorize(Roles = nameof(Role.Admin))]
        [HttpDelete("DeleteUser")]
        public async Task DeleteUser([FromBody] Guid userGuid)
        {
            await _userService.Delete(userGuid);
        }

        /// <summary>
        /// Возвращает всех менеджеров
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = nameof(Role.Admin))]
        [HttpGet("GetManagers")]
        public IEnumerable<UserModel> GetManagers()
        {
            return _userService.GetManagers();
        }

        /// <summary>
        /// Поиск и сортировка менеджеров
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = nameof(Role.Admin))]
        [HttpPost("SearchManagers")]
        public ManagerSearchResult SearchManagers([FromBody] SearchManagerModel model)
        {
            var managers = _userService.SearchManagers(model.SearchString);

            return new ManagerSearchResult(
                managers.Count,
                UserSortManager.Sort(managers, model.Field, model.Direction, model.Count, model.Offset));
        }
    }
}
