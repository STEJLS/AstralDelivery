using System;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain.Utils;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AstralDelivery.Domain
{
    /// <summary>
    /// Методы расширения для регистрации сервисов управления
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Регистрация конечной реализации сервисов управления
        /// </summary>
        /// <param name="services"> Коллекция сервисов </param>
        /// <returns>Коллекция сервисов с добавленными сервисами менеджмента</returns>
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IHashingService, HashingService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IPasswordRecoveryService, PasswordRecoveryService>();
            services.AddScoped<DataInitializer>();
            services.AddSingleton<SaltManager>();

            return services;
        }

        /// <summary>
        /// Добавление утилит необходимых для проекта 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setUp"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainUtils(this IServiceCollection services, Action<ConfigurationOptions> setUp)
        {
            var configurationOptions = new ConfigurationOptions();
            setUp(configurationOptions);
            services.AddSingleton(configurationOptions);

            return services;
        }

        /// <summary>
        /// Добавление утилит-заглушек необходимых для проекта 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setUp"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomainUtilsStub(this IServiceCollection services, Action<ConfigurationOptions> setUp)
        {
            var configurationOptions = new ConfigurationOptions();
            setUp(configurationOptions);
            services.AddSingleton(configurationOptions);

            return services;
        }
    }
}

