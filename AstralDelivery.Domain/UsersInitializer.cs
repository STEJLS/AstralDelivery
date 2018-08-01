using System;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Entities;
using System.Threading.Tasks;
using AstralDelivery.Database;
using Microsoft.EntityFrameworkCore;

namespace AstralDelivery.Domain
{
    public class UsersInitializer
    {
        public static async Task InitializeAsync(DatabaseContext dbContext, IUserService userService, ConfigurationOptions options)
        {
            if (await dbContext.Users.FirstOrDefaultAsync(u => u.Login == options.AdminLogin) == null)
            {
                string password = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 10);
                await userService.Create(options.AdminLogin, password, options.AdminEmail, Role.Admin);
            }

        }
    }
}
