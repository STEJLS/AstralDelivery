using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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