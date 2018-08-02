using Microsoft.AspNetCore.Mvc;
using AstralDelivery.Domain.Models;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер главной страницы пользователя
    /// </summary>
    public class HomeController : Controller
    {
        private readonly SessionContext _sessionContext;

        public HomeController(SessionContext sessionContext)
        {
            _sessionContext = sessionContext;
        }
        /// <summary>
        /// Главная страница
        /// </summary>
        /// <returns></returns>
        public IActionResult Default()
        {
            if (_sessionContext.Authorized)
                return View("Index");

            return View("Login");
        }
    }
}
