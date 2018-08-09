using Microsoft.AspNetCore.Mvc;
using AstralDelivery.Domain.Models;
using System;
using AstralDelivery.Domain.Abstractions;
using System.Threading.Tasks;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер-заглушка для страниц пользователя
    /// </summary>
    public class HomeController : Controller
    {
        private readonly SessionContext _sessionContext;
        private readonly IPasswordRecoveryService _recoveryService;

        public HomeController(SessionContext sessionContext, IPasswordRecoveryService recoveryService)
        {
            _sessionContext = sessionContext;
            _recoveryService = recoveryService;
        }
        /// <summary>
        /// Главная страница
        /// </summary>
        /// <returns></returns>
        public IActionResult Default()
        {
            Console.WriteLine("ghbdtn");
            if (_sessionContext.Authorized)
                return View("Index");

            return View("Login");
        }

        /// <summary>
        /// Форма восстановления пароля
        /// </summary>
        /// <param name="token"> Токен авторизации </param>
        /// <returns></returns>
        public async Task<IActionResult> PasswordRecovery([FromRoute] Guid token)
        {
            if (await _recoveryService.CheckToken(token))
                return View("PasswordRecoveryForm");

            return RedirectToAction("Default", "Home");
        }
    }
}
