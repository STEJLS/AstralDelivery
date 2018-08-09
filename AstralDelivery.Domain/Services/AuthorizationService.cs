using System;
using System.Threading.Tasks;
using AstralDelivery.Database;
using Microsoft.AspNetCore.Identity;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AstralDelivery.Domain.Models;

namespace AstralDelivery.Domain.Services
{
    /// <inheritdoc />
    public class AuthorizationService : IAuthorizationService
    {
        private readonly DatabaseContext _dbContext;
        private readonly SignInManager<User> _signInManager;
        private readonly IHashingService _hashingService;

        /// <summary />
        public AuthorizationService(DatabaseContext dbContext, SignInManager<User> signInManager, IHashingService hashingService)
        {
            _dbContext = dbContext;
            _signInManager = signInManager;
            _hashingService = hashingService;
        }

        /// <inheritdoc />
        public async Task<UserModel> Login(string email, string password, bool rememberMe)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Некорректный логин/пароль");
            }

            email = email.Trim().ToLower();
            password = _hashingService.Get(password);

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email && u.Password == password && u.IsDeleted == false);
            if (user == null)
            {
                throw new InvalidOperationException("Пользователь с указаной парой Логин/Пароль не найден.");
            }

            await _signInManager.SignInAsync(user, rememberMe);
            return new UserModel(user);
        }

        /// <inheritdoc />
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
