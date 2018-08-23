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

            string password = GetNewPassword();
            user = new User(model.Email, _hashingService.Get(password), GetDeliveryPointNameForUser(point), model.Surname, model.Name, model.Patronymic, model.Phone, model.Role, model.DeliveryPointGuid);
            await _dbContext.Users.AddAsync(user);
            await _mailService.SendAsync(model.Email, password, "Пароль от учетной записи");
            await _dbContext.SaveChangesAsync();

            return user.UserGuid;
        }

        /// <inheritdoc />
        public async Task<Guid> CreateCourier(UserInfo model)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                throw new Exception("Пользователь с указанной почтой уже существует");
            }

            User manager = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid && u.IsDeleted == false);

            string password = GetNewPassword();
            user = new User(model.Email, password, manager.DeliveryPointName, model.Surname, model.Name, model.Patronymic, model.Phone, Role.Сourier, manager.DeliveryPointGuid);
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
            user.Phone = model.Phone;

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
            user.Phone = model.Phone;
            user.Role = model.Role;
            user.DeliveryPointGuid = model.DeliveryPointGuid;

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteManager(Guid UserGuid)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == UserGuid && u.IsDeleted == false && u.Role == Role.Manager);
            if (user == null)
            {
                throw new Exception("Пользователя с таким идентификатором не существует");
            }

            user.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteCourier(Guid UserGuid)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == UserGuid && u.IsDeleted == false && u.Role == Role.Сourier);
            if (user == null)
            {
                throw new Exception("Пользователя с таким идентификатором не существует");
            }

            user.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task ChangePassword(string oldPassword, string newPassword)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid && u.IsDeleted == false);

            if (user.Password != _hashingService.Get(oldPassword))
            {
                throw new Exception("Неверный пароль");
            }

            user.Password = _hashingService.Get(newPassword);
            user.IsActivated = true;
            await _dbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public IEnumerable<User> SearchManagers(string searchString)
        {
            var managers = _dbContext.Users.AsNoTracking()
                    .Where(u => u.Role == Role.Manager)
                    .Where(u => u.IsDeleted == false);

            return UserFilter(managers, searchString);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<User>> SearchCouriers(string searchString)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid && u.IsDeleted == false);

            var couriers = _dbContext.Users.AsNoTracking()
                    .Where(u => u.Role == Role.Сourier)
                    .Where(u => u.DeliveryPointGuid == user.DeliveryPointGuid)
                    .Where(u => u.IsDeleted == false);

            return UserFilter(couriers, searchString);
        }

        /// <inheritdoc />
        public async Task<User> GetUserInfo(Guid guid)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserGuid == guid && u.IsDeleted == false);
            if (user == null)
            {
                throw new Exception("Пользователя с таким идентификатором не существует");
            }

            return user;
        }

        private IEnumerable<User> UserFilter(IEnumerable<User> users, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.Trim().ToUpper();
                return users.Where(u => u.Email.ToUpper().Contains(searchString) ||
                     u.DeliveryPointName.ToUpper().Contains(searchString) ||
                    $"{u.Surname} {u.Name} {u.Patronymic}".ToUpper().Contains(searchString));
            }

            return users;
        }


        private string GetNewPassword()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
        }

        private string GetDeliveryPointNameForUser(DeliveryPoint point)
        {
            return $"{point.Name} {point.City}";
        }

        public async Task<List<User>> GetFreeCourier(DateTime dateTime)
        {
            User manager = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserGuid == _sessionContext.UserGuid && u.IsDeleted == false);

            return _dbContext.Users.AsNoTracking().Where(u => u.DeliveryPointGuid == manager.DeliveryPointGuid)
                .Where(u => _dbContext.Products.AsNoTracking()
                    .Where(p => p.CourierGuid == u.UserGuid)
                    .Where(p => p.DateTime.Date == dateTime.Date)
                    .Count() < 6)
                    .ToList();
        }
    }
}
