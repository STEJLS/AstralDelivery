﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using AstralDelivery.Database;
using AstralDelivery.Utils.ExceptionFilter;
using AstralDelivery.MailService;
using Microsoft.Extensions.DependencyInjection;
using AstralDelivery.Domain;
using AstralDelivery.Identity;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.Logging;

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
            services.AddAstralDeliveryIdentity();

            //Логика
            services.AddDomainServices();
            if (Environment.IsDevelopment())
            {
                services.AddDomainUtilsStub(options =>
                {
                    options.Salt = Configuration["Salt"];
                    options.ServiceEmail = Configuration["ServiceEmail:Email"];
                    options.ServicePassword = Configuration["ServiceEmail:Password"];
                    options.AdminEmail = Configuration["Admin:Email"];
                    options.AdminPassword = Configuration["Admin:Password"];
                    options.PasswordRecoveryTokenLifeTime = int.Parse(Configuration["PasswordRecovery:TokenLifeTime"]);
                });
                services.AddMailServicesStub();
            }
            else
            {
                services.AddDomainUtils(options =>
                {
                    options.Salt = Configuration["Salt"];
                    options.ServiceEmail = Configuration["ServicEmail:Email"];
                    options.ServicePassword = Configuration["ServicEmail:Password"];
                    options.AdminEmail = Configuration["Admin:Email"];
                    options.AdminPassword = Configuration["Admin:Password"];
                    options.PasswordRecoveryTokenLifeTime = int.Parse(Configuration["PasswordRecovery:TokenLifeTime"]);
                });

                services.AddMailServices();
            }

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    options.Filters.AddService(typeof(ExceptionFilter));
                });

            services.AddScoped<ExceptionFilter>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.RoutePrefix = "api/help";
            });

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "PasswordRecovery",
                    "Home/PasswordRecovery/{token}",
                    new { controller = "Home", action = "PasswordRecovery" });
                routes.MapRoute(
                    "default",
                    "{*catchall}",
                    new { controller = "Home", action = "Default" });
            });
        }
    }
}
