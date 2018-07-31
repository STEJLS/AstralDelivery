using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using AstralDelivery.Database;
using Microsoft.Extensions.DependencyInjection;
using AstralDelivery.Domain;
using AstralDelivery.Identity;
using Microsoft.Extensions.Configuration;

namespace AstralDelivery
{
    public class Startup
    {
        private IHostingEnvironment Environment { get; }
        private IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
                .AddApplicationInsightsSettings(true);

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseContext(Configuration.GetConnectionString("DefaultConnection"));

            // Identity
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromHours(24));
            services.AddAstralNotesIdentity();

            //Логика
            services.AddDomainServices();
            if (Environment.IsDevelopment())
            {
                services.AddDomainUtilsStub(options =>
                {
                    options.Salt = Configuration["Salt"];
                });
            }
            else
            {
                services.AddDomainUtils(options =>
                {
                    options.Salt = Configuration["Salt"];
                });
            }

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
        }
    }
}
