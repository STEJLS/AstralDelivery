﻿using AstralDelivery.Domain.Entities;
using AstralDelivery.Domain.Models;
using AstralDelivery.Identity.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace AstralDelivery.Identity
{
    /// <summary>
    /// Методы расширения для регистрации сервисов Identity
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Регистрация сервисов Identity
        /// </summary>
        /// <param name="services"> <see cref="IServiceCollection"/> </param>
        /// <returns> <see cref="IServiceCollection"/> </returns>
        public static IServiceCollection AddAstralDeliveryIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, AccessPolicy>()
                .AddUserStore<IdentityUserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();

            services.AddProvider<SessionContext, SessionContextProvider>();

            return services;
        }
    }
}
