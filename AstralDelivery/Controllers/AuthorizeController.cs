using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер управления авторизацией
    /// </summary>
    [Route("Authorize")]
    public class AuthorizeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly SignInManager<User> _signInManager;

        /// <summary />
        public AuthorizeController(SignInManager<User> signInManager, IAuthorizationService authorizationService)
        {
            _signInManager = signInManager;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"> Модель авторизации </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<User> Login([FromBody]LoginModel model)
        {
            return await _authorizationService.Login(model.Email, model.Password, model.RememberMe);
        }

        /// <summary>
        /// Вывод из системы
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpDelete]
        public async void Logout()
        {
            await _authorizationService.Logout();
        }
    }
}
