using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq;
using AstralDelivery.Domain.Models;
using System.Collections.Generic;
using AstralDelivery.MailService.Abstractions;

namespace AstralDelivery.Domain.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IHashingService _hashingService;
        private readonly IMailService _mailService;
        private readonly SessionContext _sessionContext;

        /// <summary />
        public UserService(DatabaseContext databaseContext, IHashingService hashingService, IMailService mailService, SessionContext sessionContext)
        {
            _hashingService = hashingService;
            _mailService = mailService;
            _dbContext = databaseContext;
            _sessionContext = sessionContext;
        }

        /// <inheritdoc />
        public async Task<Guid> CreateAdmin(string email, string password, Guid deliveryPointGuid)
        {
            User user = new User(email, _hashingService.Get(password), Role.Admin, deliveryPointGuid);
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user.UserGuid;
        }

        /// <inheritdoc />
        public async Task<Guid> Create(UserInfo model)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                throw new Exception("Пользователь с указанной почтой уже существует");
            }

            string password = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            user = new User(model.Email, _hashingService.Get(password), model.City, model.Surname, model.Name, model.Patronymic, model.Role, model.DeliveryPointGuid);
            await _dbContext.Users.AddAsync(user);
            await _mailService.SendAsync(model.Email, password, "Пароль от учетной записи");
            await _dbContext.SaveChangesAsync();

            return user.UserGuid;
        }

        /// <inheritdoc />
        public async Task Edit(Guid guid, UserInfo model)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == guid && u.IsDeleted == false);
            if (user == null)
            {
                throw new Exception("Пользователя с таким идентификатором не существует");
            }

            user.Email = model.Email;
            user.Surname = model.Surname;
            user.Name = model.Name;
            user.Patronymic = model.Patronymic;

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task AdminEdit(Guid guid, UserInfo userModel)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == guid && u.IsDeleted == false);
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IEnumerable<User> SearchManagers(string searchString)
        {
            var managers = _dbContext.Users.AsNoTracking()
                    .Where(u => u.Role == Role.Manager)
                    .Where(u => u.IsDeleted == false);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToUpper();
                managers = managers.Where(u => u.Email.ToUpper().Contains(searchString) ||
                     u.City.ToUpper().Contains(searchString) ||
                    $"{u.Surname} {u.Name} {u.Patronymic}".ToUpper().Contains(searchString));
            }

            return managers;
        }
    }
}
