using System;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Entities;
using System.Threading.Tasks;
using AstralDelivery.Database;
using Microsoft.EntityFrameworkCore;

namespace AstralDelivery.Domain
{
    public class DataInitializer
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IUserService _userService;
        private readonly ConfigurationOptions _options;

        public DataInitializer(DatabaseContext databaseContext, IUserService userService, ConfigurationOptions options)
        {
            _databaseContext = databaseContext;
            _userService = userService;
            _options = options;
        }

        public async Task InitializeAsync()
        {
            await InitializeAdmin();
        }

        private async Task InitializeAdmin()
        {
            if (!await _databaseContext.Users.AnyAsync())
            {
                await _userService.Create(_options.AdminEmail, _options.AdminPassword, Role.Admin);
            }
        }
    }
}