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

            var point = await _dbContext.DeliveryPoints.FirstOrDefaultAsync(p => p.Guid == model.DeliveryPointGuid);
            if (point == null)
            {
                throw new Exception("Указанного пункта выдачи не существует");
            }

            string password = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
            user = new User(model.Email, _hashingService.Get(password), GetDeliveryPointNameForUser(point), model.Surname, model.Name, model.Patronymic, model.Role, model.DeliveryPointGuid);
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
        public async Task AdminEdit(Guid guid, UserInfo model)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == guid && u.IsDeleted == false);
            if (user == null)
            {
                throw new Exception("Пользователя с таким идентификатором не существует");
            }

            var point = await _dbContext.DeliveryPoints.FirstOrDefaultAsync(p => p.Guid == model.DeliveryPointGuid);
            if (point == null)
            {
                throw new Exception("Указанного пункта выдачи не существует");
            }

            user.Email = model.Email;
            user.DeliveryPointName = GetDeliveryPointNameForUser(point);
            user.Surname = model.Surname;
            user.Name = model.Name;
            user.Patronymic = model.Patronymic;
            user.Role = model.Role;
            user.DeliveryPointGuid = model.DeliveryPointGuid;

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
                     u.DeliveryPointName.ToUpper().Contains(searchString) ||
                    $"{u.Surname} {u.Name} {u.Patronymic}".ToUpper().Contains(searchString));
            }

            return managers;
        }

        private string GetDeliveryPointNameForUser(DeliveryPoint point)
        {
            return $"{point.Name} {point.City}";
        }
    }
}
