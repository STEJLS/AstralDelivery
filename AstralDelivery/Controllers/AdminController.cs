using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
        [HttpPost("EditUser")]
        public async Task EditUser([FromBody] UserModel model)
        {
            await _userService.Edit(model);
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
    }
}
