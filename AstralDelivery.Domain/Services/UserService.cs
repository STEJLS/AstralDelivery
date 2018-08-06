using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Database;
using AstralDelivery.MailService;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using AstralDelivery.Domain.Models;
using System.Collections.Generic;

namespace AstralDelivery.Domain.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IHashingService _hashingService;
        private readonly MailSender _mailSender;
        private readonly SessionContext _sessionContext;

        /// <summary>
        /// Конструктор с двумя параметрами DatabaseContext и IHashingService
        /// </summary>
        /// <param name="databaseContext"> <see cref="DatabaseContext"/> </param>
        /// <param name="hashingService"> <see cref="IHashingService"/> </param>
        /// <param name="mailService" cref="IMailService"/>
        public UserService(DatabaseContext databaseContext, IHashingService hashingService, MailSender mailSender, SessionContext sessionContext)
        {
            _hashingService = hashingService;
            _mailSender = mailSender;
            _dbContext = databaseContext;
            _sessionContext = sessionContext;
        }

        /// <inheritdoc />
        public async Task Create(string email, string password)
        {
            User user = new User(email, _hashingService.Get(password), Role.Admin);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task Create(string email, string city, string surname, string name, string patronymic, Role role)
        {
            string password = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            User user = new User(email, _hashingService.Get(password), city, surname, name, patronymic, role);
            await _dbContext.Users.AddAsync(user);
            await _mailSender.SendAsync(email, password, "Пароль от учетной записи");
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public IEnumerable<UserModel> GetManagers()
        {
            var managers = _dbContext.Users.Where(u => u.Role == Role.Manager && u.IsDeleted == false).Select(u => new UserModel(u)).ToList();
            return managers;
        }

        /// <inheritdoc />
        public async Task Edit(UserModel userModel)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == userModel.UserGuid && u.IsDeleted == false);
            if (user == null)
            {
                throw new Exception("Пользователя с таким идентификатором не существует");
            }

            user.Email = userModel.Email;
            user.City = userModel.City;
            user.Surname = userModel.Surname;
            user.Name = userModel.Name;
            user.Patronymic = userModel.Patronymic;
            user.Role = userModel.Role;

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task Delete(Guid UserGuid)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == UserGuid && u.IsDeleted == false);
            if (user == null)
            {
                throw new Exception("Пользователя с таким идентификатором не существует");
            }

            user.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Изменяет пароль пользователя
        /// </summary>
        /// <param name="model"> <see cref="ChangePasswordModel"/> </param>
        /// <returns></returns>
        public async Task ChangePassword(ChangePasswordModel model)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid && u.IsDeleted == false);
            if (user == null)
            {
                throw new Exception("Пользователя с таким идентификатором не существует");
            }

            if (user.Password != _hashingService.Get(model.OldPassword))
            {
                throw new Exception("Неверный пароль");
            }

            user.Password = _hashingService.Get(model.NewPassword);
            user.IsActivated = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
