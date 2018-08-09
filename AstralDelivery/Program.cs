using AstralDelivery.Database;
using AstralDelivery.Utils;
using AstralDelivery.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace AstralDelivery
{
    /// <summary />
    public class Program
    {
        /// <summary />
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.MigrateDatabase<DatabaseContext>()
                .SetUpWithService<DataInitializer>(init =>
                Task.WaitAll(init.InitializeAsync()));

            host.Run();
        }

        /// <summary />
        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();
    }
}