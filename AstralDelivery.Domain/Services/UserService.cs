using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Database;
using AstralDelivery.MailService;
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

        /// <summary>
        /// Конструктор с двумя параметрами DatabaseContext и IHashingService
        /// </summary>
        /// <param name="databaseContext"> <see cref="DatabaseContext"/> </param>
        /// <param name="hashingService"> <see cref="IHashingService"/> </param>
        /// <param name="mailService" cref="IMailService"/>
        public UserService(DatabaseContext databaseContext, IHashingService hashingService, MailSender mailSender)
        {
            _hashingService = hashingService;
            _mailSender = mailSender;
            _dbContext = databaseContext;
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
            var managers = _dbContext.Users.Where(u => u.Role == Role.Manager).Select(u => new UserModel(u)).ToList();
            return managers;
        }
    }
}
