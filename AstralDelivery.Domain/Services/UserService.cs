using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AstralDelivery.Domain.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IHashingService _hashingService;
        private readonly IMailService _mailService;

        /// <summary>
        /// Конструктор с двумя параметрами DatabaseContext и IHashingService
        /// </summary>
        /// <param name="databaseContext"> <see cref="DatabaseContext"/> </param>
        /// <param name="hashingService"> <see cref="IHashingService"/> </param>
        /// <param name="mailService" cref="IMailService"/>
        public UserService(DatabaseContext databaseContext, IHashingService hashingService, IMailService mailService)
        {
            _hashingService = hashingService;
            _mailService = mailService;
            _dbContext = databaseContext;
        }

        /// <inheritdoc />
        public async Task Create(string login, string password, string email, Role role)
        {
            User user = new User(login, _hashingService.Get(password), email, role);
            await _dbContext.Users.AddAsync(user);
            await _mailService.Send(password, "Пароль от аккаунта");
            await _dbContext.SaveChangesAsync();  
        }
    }
}
