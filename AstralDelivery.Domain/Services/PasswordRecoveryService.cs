using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Entities;
using AstralDelivery.Database;
using AstralDelivery.MailService.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace AstralDelivery.Domain.Services
{
    /// <inheritdoc />
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IHashingService _hashingService;
        private readonly ConfigurationOptions _options;
        private readonly IMailService _mailService;

        public PasswordRecoveryService(DatabaseContext dbContext, IHashingService hashingService, ConfigurationOptions options, IMailService mailService)
        {
            _dbContext = dbContext;
            _hashingService = hashingService;
            _options = options;
            _mailService = mailService;
        }

        /// <inheritdoc />
        public async Task CreateToken(string email, string host)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);
            if (user == null)
            {
                throw new Exception("Пользователя с такой почтой не существует.");
            }

            var recovery = await _dbContext.PasswordRecoveries.FirstOrDefaultAsync(r => r.UserGuid == user.UserGuid);
            if (recovery != null)
            {
                _dbContext.PasswordRecoveries.Remove(recovery);
            }

            recovery = new PasswordRecovery(user.UserGuid);
            await _dbContext.PasswordRecoveries.AddAsync(recovery);
            await _dbContext.SaveChangesAsync();

            await _mailService.SendAsync(email, "Ссылка для восстановления пароля: http://" + host + "/Home/PasswordRecovery/" + recovery.Token.ToString(), "Восстановление пароля");
        }

        /// <inheritdoc />
        public async Task ChangePassword(Guid token, string newPassword)
        {
            var recovery = await _dbContext.PasswordRecoveries.FirstOrDefaultAsync(r => r.Token == token);
            if (recovery == null || !ValidatePasswordRecovery(recovery))
            {
                throw new Exception("Токен устарел.");
            }

            _dbContext.Entry(recovery).Navigation("User").Load();

            recovery.User.Password = _hashingService.Get(newPassword);
            _dbContext.PasswordRecoveries.Remove(recovery);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckToken(Guid token)
        {
            var recovery = await _dbContext.PasswordRecoveries.FirstOrDefaultAsync(r => r.Token == token);
            if (recovery != null)
            {
                return ValidatePasswordRecovery(recovery);
            }

            return false;
        }

        private bool ValidatePasswordRecovery(PasswordRecovery recovery)
        {
            var diff = DateTime.Now - recovery.CreationDate;

            return diff < new TimeSpan(0, _options.PasswordRecoveryTokenLifeTime, 0);
        }
    }
}
