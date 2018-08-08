using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер управления аккаунтом
    /// </summary>
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Изменяет пароль пользователя
        /// </summary>
        /// <param name="model"> <see cref="ChangePasswordModel"/> </param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task ChangePassword([FromBody] ChangePasswordModel model)
        {
            await _userService.ChangePassword(model);
        }

        /// <summary>
        /// Редактирует данные пользователя
        /// </summary>
        /// <param name="model"> <see cref="EditUserModel"/> </param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("Edit/{userGuid}")]
        public async Task Edit([FromRoute] Guid userGuid, [FromBody] UserInfo model)
        {
            await _userService.Edit(userGuid, model);
        }
    }
}
