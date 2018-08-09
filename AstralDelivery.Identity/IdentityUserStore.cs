using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AstralDelivery.Database;
using AstralDelivery.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AstralDelivery.Identity
{
    /// <summary>
    /// Хранилище удостоверений на основе сертификатов абонента
    /// </summary>
    public class IdentityUserStore : IUserLockoutStore<User>, IUserRoleStore<User>
    {
        private readonly DatabaseContext _context;

        /// <summary/>
        public IdentityUserStore(DatabaseContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary/>
        public Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary/>
        public Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение роли
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            var result = (IList<string>)new List<string>();
            result.Add(user.Role.ToString());
            return Task.FromResult(result);
        }

        /// <summary>
        /// Проверка пользователя на наличие роли
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="roleName">Название роли</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Role.ToString() == roleName);
        }

        /// <summary>
        /// Получение коллекции пользователей находящихся в роли
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserGuid.ToString());
        }

        /// <inheritdoc />
        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserGuid.ToString());
        }

        /// <inheritdoc />
        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserGuid.ToString());
        }

        /// <inheritdoc />
        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var guid = Guid.Parse(userId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserGuid == guid && u.IsDeleted == false, cancellationToken);
            return user;
        }

        /// <inheritdoc />
        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserGuid.ToString() == normalizedUserName && u.IsDeleted == false,
                cancellationToken);
            return user;
        }

        /// <inheritdoc />
        public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult((DateTimeOffset?)null);
        }

        /// <inheritdoc />
        public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        /// <inheritdoc />
        public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(false);
        }

        /// <inheritdoc />
        public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}