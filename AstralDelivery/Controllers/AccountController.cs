using System;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер управления аккаунтом
    /// </summary>
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IRecoveryPasswordService _recoveryService;

        public AccountController(IRecoveryPasswordService recoveryService)
        {
            _recoveryService = recoveryService;
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
        /// Изменяет пароль пользователя
        /// </summary>
        /// <param name="model"> Модель PasswordRecovery </param>
        /// <returns></returns>
        [HttpPut("RecoveryPasswordChange")]
        public async Task RecoveryPasswordChange([FromBody] PasswordRecoveryModel model)
        {
            await _recoveryService.ChangePassword(model.Token, model.NewPassword);
        }
    }
}
