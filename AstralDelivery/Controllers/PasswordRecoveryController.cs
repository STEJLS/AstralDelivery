using System.Threading.Tasks;
using AstralDelivery.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using AstralDelivery.Domain.Abstractions;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер управляющий восстановлением пароля
    /// </summary>
    [Route("PasswordRecovery")]
    public class PasswordRecoveryController : Controller
    {
        private readonly IPasswordRecoveryService _recoveryService;

        /// <summary />
        public PasswordRecoveryController(IPasswordRecoveryService recoveryService)
        {
            _recoveryService = recoveryService;
        }

        /// <summary>
        /// Создает токен для восстановления пароля
        /// </summary>
        /// <param name="model"> <see cref="RecoveryTokenCreationModel"/> </param>
        /// <returns></returns>
        [HttpPost("CreateToken")]
        public async Task CreateToken([FromBody] RecoveryTokenCreationModel model)
        {
            await _recoveryService.CreateToken(model.Email, HttpContext.Request.Host.Value);
        }

        /// <summary>
        /// Восстанавливает пароль пользователя
        /// </summary>
        /// <param name="model"> <see cref="PasswordRecoveryModel"/> </param>
        /// <returns></returns>
        [HttpPut("ChangePassword")]
        public async Task ChangePassword([FromBody] PasswordRecoveryModel model)
        {
            await _recoveryService.ChangePassword(model.Token, model.NewPassword);
        }
    }
}
