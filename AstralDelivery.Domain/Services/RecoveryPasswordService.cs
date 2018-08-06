using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Entities;
using AstralDelivery.MailService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using AstralDelivery.Database;

namespace AstralDelivery.Domain.Services
{
    /// <inheritdoc />
    public class RecoveryPasswordService : IRecoveryPasswordService
    {
        private readonly DatabaseContext _dbContext;
        private readonly IHashingService _hashingService;
        private readonly ConfigurationOptions _options;
        private readonly MailSender _mailSender;

        public RecoveryPasswordService(DatabaseContext dbContext, IHashingService hashingService, ConfigurationOptions options, MailSender mailSender)
        {
            _dbContext = dbContext;
            _hashingService = hashingService;
            _options = options;
            _mailSender = mailSender;
        }

        /// <inheritdoc />
        public async Task CreateToken(string email, string host)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted == false);
            if (user == null)
            {
                throw new Exception("Пользователя с такой почтой не существует.");
            }

            var recovery = await _dbContext.passwordRecoveries.FirstOrDefaultAsync(r => r.UserGuid == user.UserGuid);
            if (recovery != null)
            {
                _dbContext.passwordRecoveries.Remove(recovery);
            }

            recovery = new PasswordRecovery(user.UserGuid);
            await _dbContext.passwordRecoveries.AddAsync(recovery);
            await _dbContext.SaveChangesAsync();

            await _mailSender.SendAsync(email, "Ссылка для восстановления пароля: http://" + host + "/Account/CheckToken?token=" + recovery.Token.ToString(), "Восстановление пароля");

        }

        /// <inheritdoc />
        public async Task ChangePassword(Guid token, string newPassword)
        {
            var recovery = await _dbContext.passwordRecoveries.FirstOrDefaultAsync(r => r.Token == token);
            if (recovery == null || !ValidatePasswordRecovery(recovery))
            {
                throw new Exception("Токен устарел.");
            }


            _dbContext.Entry(recovery).Navigation("User").Load();

            recovery.User.Password = _hashingService.Get(newPassword);
            _dbContext.passwordRecoveries.Remove(recovery);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckToken(Guid token)
        {
            var recovery = await _dbContext.passwordRecoveries.FirstOrDefaultAsync(r => r.Token == token);
            if (recovery != null)
            {
                return ValidatePasswordRecovery(recovery);
            }

            return false;
        }

        private bool ValidatePasswordRecovery(PasswordRecovery recovery)
        {
            var diff = DateTime.Now - recovery.CreationDate;
            if (diff < new TimeSpan(0, _options.PasswordRecoveryTokenLifeTime, 0))
            {
                return true;
            }

            return false;
        }
    }
}
