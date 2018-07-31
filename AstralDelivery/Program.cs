using AstralDelivery.Database;
using AstralDelivery.Utils;
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
            host.MigrateDatabase<DatabaseContext>();
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