using System;
using System.Threading.Tasks;
using AstralDelivery.Database;
using Microsoft.AspNetCore.Identity;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AstralDelivery.Domain.Services
{
    /// <inheritdoc />
    public class AuthorizationService : IAuthorizationService
    {
        private readonly DatabaseContext _dbContext;
        private readonly SignInManager<User> _signInManager;
        private readonly IHashingService _hashingService;

        /// <summary>
        /// Конструктор с двумя параметрами DatabaseContext и IHashingService
        /// </summary>
        /// <param name="dbContext"> <see cref="DatabaseContext"/> </param>
        /// <param name="hashingService"> <see cref="IHashingService"/> </param>
        public AuthorizationService(DatabaseContext dbContext, SignInManager<User> signInManager, IHashingService hashingService)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _hashingService = hashingService;
        }

        /// <inheritdoc />
        public async Task<User> Login(string email, string password, bool rememberMe)
        {
            if (email == null || password == null)
            {
                throw new Exception("Некорректный логин/пароль");
            }

            email = email.Trim().ToLower();
            password = _hashingService.Get(password);

            var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.Email.ToLower() == email && a.Password == password);
            if (user == null)
            {
                throw new InvalidOperationException("Пользователь с указаной парой Логин/Пароль не найден.");
            }

            await _signInManager.SignInAsync(user, rememberMe);
            return user;
        }

        /// <inheritdoc />
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
