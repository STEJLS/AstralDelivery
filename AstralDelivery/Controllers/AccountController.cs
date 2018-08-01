using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.ViewModels;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер управления аккаунтом
    /// </summary>
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<User> _signInManager;

        /// <summary />
        public AccountController(IUserService userService, SignInManager<User> signInManager,
            IAuthorizationService authorizationService)
        {
            _userService = userService;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
        }

        ///// <summary/>
        //[HttpGet("Login")]
        //public async Task<IActionResult> Login()
        //{
        //    return View();
        //}

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"> Модель авторизации </param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task Login([FromQuery]LoginViewModel model)
        {
            await _authorizationService.Login(model.Login, model.Password, model.RememberMe);

            if (string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                Redirect(model.ReturnUrl);
            }
            else
            {
                RedirectToAction("Home", "Index");
            }
        }

        /// <summary>
        /// Вывод из системы
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authorizationService.Logout();
            return RedirectToAction("Account", "Login");
        }
    }
}
