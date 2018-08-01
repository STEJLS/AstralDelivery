using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task Login(string login, string password, bool rememberMe)
        {
            if (login == null || password == null)
            {
                throw new Exception("Некорректный логин/пароль");
            }

            login = login.Trim().ToLower();
            password = _hashingService.Get(password);

            var user = await _dbContext.Users.FirstOrDefaultAsync(a => a.Login.ToLower() == login && a.Password == password);
            if (user == null)
            {
                throw new InvalidOperationException("Пользователь с указаной парой Логин/Пароль не найден.");
            }

            await _signInManager.SignInAsync(user, rememberMe);
        }

        /// <inheritdoc />
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
