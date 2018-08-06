using System;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер управления аккаунтом
    /// </summary>
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IRecoveryPasswordService _recoveryService;
        private readonly IUserService _userService;

        public AccountController(IRecoveryPasswordService recoveryService, IUserService userService)
        {
            _recoveryService = recoveryService;
            _userService = userService;
        }

        /// <summary>
        /// Проверяет токен восстановления
        /// </summary>
        /// <param name="token"> Токен</param>
        /// <returns></returns>
        [HttpGet("CheckToken")]
        public async Task<IActionResult> CheckToken([FromQuery] Guid token)
        {
            if (await _recoveryService.CheckToken(token))
                return View("RecoveryPasswordForm");

            return RedirectToAction("Default", "Home");
        }

        /// <summary>
        /// Создает токен для восстановления пароля
        /// </summary>
        /// <param name="email"> Почта для восстановления </param>
        /// <returns></returns>
        [HttpPost("RecoveryTokenCreation")]
        public async Task RecoveryTokenCreation([FromBody] string email)
        {
            await _recoveryService.CreateToken(email, HttpContext.Request.Host.Value);
        }

        /// <summary>
        /// Восстанавливает пароль пользователя
        /// </summary>
        /// <param name="model"> Модель PasswordRecovery </param>
        /// <returns></returns>
        [HttpPut("RecoveryPasswordChange")]
        public async Task RecoveryPasswordChange([FromBody] PasswordRecoveryModel model)
        {
            await _recoveryService.ChangePassword(model.Token, model.NewPassword);
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
    }
}
