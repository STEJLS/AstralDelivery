using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using System.Threading.Tasks;
using AstralDelivery.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using AstralDelivery.Domain.Entities;

namespace AstralDelivery.Domain
{
    public class DataInitializer
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IUserService _userService;
        private readonly ConfigurationOptions _options;
        private readonly IDeliveryPointService _pointService;

        public DataInitializer(DatabaseContext databaseContext, IUserService userService, IDeliveryPointService pointService, ConfigurationOptions options)
        {
            _databaseContext = databaseContext;
            _userService = userService;
            _pointService = pointService;
            _options = options;
        }

        public async Task InitializeAsync()
        {
            await InitializeAdmin();
        }

        private async Task InitializeAdminDeliveryPoint()
        {
            await InitializeAdmin();
        }

        private async Task InitializeAdmin()
        {
            if (!await _databaseContext.Users.AnyAsync())
            {
                Guid pointGuid = await _pointService.Create(new DeliveryPointInfo { Name = "Admin Point", WorksSchedule = new List<WorkTime>() });
                await _userService.CreateAdmin(_options.AdminEmail, _options.AdminPassword, pointGuid);
            }
        }
    }
}