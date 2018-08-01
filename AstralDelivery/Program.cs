using AstralDelivery.Database;
using AstralDelivery.Utils;
using AstralDelivery.Domain.Abstractions;
using AstralDelivery.Domain.Models;
using AstralDelivery.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AstralDelivery
{
    /// <summary />
    public class Program
    {
        /// <summary />
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.MigrateDatabase<DatabaseContext>();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var dbcontext = services.GetRequiredService<DatabaseContext>();
                var userService = services.GetRequiredService<IUserService>();
                var options = services.GetRequiredService<ConfigurationOptions>();
                UsersInitializer.InitializeAsync(dbcontext, userService, options);
            }

            BuildWebHost(args).Run();
        }

        /// <summary />
        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}