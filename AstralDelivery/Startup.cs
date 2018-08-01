using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using AstralDelivery.Database;
using AstralDelivery.Utils;
using Microsoft.Extensions.DependencyInjection;
using AstralDelivery.Domain;
using AstralDelivery.Identity;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;

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
                    options.ServiceEmail = Configuration["ServiceEmail"];
                    options.ServicePassword = Configuration["ServicePassword"];
                    options.AdminEmail = Configuration["AdminEmail"];
                    options.AdminLogin = Configuration["AdminLogin"];
                });
            }
            else
            {
                services.AddDomainUtils(options =>
                {
                    options.Salt = Configuration["Salt"];
                    options.ServiceEmail = Configuration["ServiceEmail"];
                    options.ServicePassword = Configuration["ServicePassword"];
                    options.AdminEmail = Configuration["AdminEmail"];
                    options.AdminLogin = Configuration["AdminLogin"];
                });
            }

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.AddMvc()
                .AddMvcOptions(options =>
                {
                    options.Filters.AddService(typeof(ErrorHandler));
                });

            services.AddScoped<ErrorHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                options.RoutePrefix = "api/help";
            });
            
            app.UseStaticFiles();
            app.UseMvc();
            app.UseAuthentication();
            app.UseSession();
        }
    }
}
