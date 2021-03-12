using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FunWithAspNetCoreConfiguration
{
    public class Startup
    {
        // Configuration providers read configuration data from key-value pairs using a variety of configuration sources:
        // * Settings files, such as appsettings.json
        // * Environment variables
        // * Azure Key Vault
        // * Azure App Configuration
        // * Command-line arguments
        // * Custom providers, installed or created
        // * Directory files
        // * In-memory .NET objects

        public IConfiguration AppConfiguration { get; set; }

        public Startup()
        {
            var configBuilder = new ConfigurationBuilder();

            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "name", "bill" },
                { "surname", "gates" }
            });

            this.AppConfiguration = configBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Actions are either conventionally-routed or attribute-routed.
            // * conventionally-routed: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#cr
            // Conventional routing typically used with controllers and views.
            // * attribute-routed: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-5.0#ar
            // Attribute routing used with REST APIs. If you're primarily interested in routing for REST APIs, jump to the Attribute routing for REST APIs section.

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}