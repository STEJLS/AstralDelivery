using Microsoft.Extensions.DependencyInjection;
using AstralDelivery.Domain.Abstractions;

namespace AstralDelivery.MailService
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
        public static IServiceCollection AddMailService(this IServiceCollection services)
        {
            services.AddScoped<IMailService, MailService>();

            return services;
        }
    }
}
