using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using AstralDelivery.Models;

namespace AstralDelivery.Controllers
{
    /// <summary>
    /// Контроллер управления авторизацией
    /// </summary>
    [Route("Authorize")]
    public class AuthorizeController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        /// <summary />
        public AuthorizeController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"> <see cref="LoginModel"/> </param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<UserModel> Login([FromBody] LoginModel model)
        {
            return new UserModel(await _authorizationService.Login(model.Email, model.Password, model.RememberMe));
        }

        /// <summary>
        /// Вывод из системы
        /// </summary>
        /// <returns></returns>
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpDelete("Logout")]
        public async void Logout()
        {
            await _authorizationService.Logout();
        }
    }
}
