using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;
using AstralDelivery.Models;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер управления аккаунтом
    /// </summary>
    [Authorize]
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
        [HttpPut("ChangePassword")]
        public async Task ChangePassword([FromBody] ChangePasswordModel model)
        {
            await _userService.ChangePassword(model.OldPassword, model.NewPassword);
        }

        /// <summary>
        /// Редактирует данные пользователя
        /// </summary>
        /// <param name="model"> <see cref="EditUserModel"/> </param>
        /// <returns></returns>
        [HttpPut("Edit/{userGuid}")]
        public async Task EditProfile([FromRoute] Guid userGuid, [FromBody] UserInfo model)
        {
            await _userService.Edit(userGuid, model);
        }
    }
}
