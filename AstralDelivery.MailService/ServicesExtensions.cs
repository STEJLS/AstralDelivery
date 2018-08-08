using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using AstralDelivery.MailService.Abstractions;
using AstralDelivery.MailService.Services;

namespace AstralDelivery.MailService
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddMailServices(this IServiceCollection services)
        {
            services.AddSingleton<IMailService, MailService>();
            return services;
        }

        public static IServiceCollection AddMailServicesStub(this IServiceCollection services)
        {
            services.AddSingleton<IMailService, MailServiceStub>();
            return services;
        }
    }
}
