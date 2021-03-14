using System.Collections.Generic;
using FunWithAspNetCoreConfiguration.Infrastructure;
using FunWithAspNetCoreConfiguration.Options;
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

        // There are few types of application configuration:
        // * Basic - data will be stored in memory. Use Microsoft.Extensions.Configuration namespace.
        // * JSON - data will be stored in JSON. Use Microsoft.Extensions.Configuration.Json namespace.
        // * Command line - data will be added via command line. Use Microsoft.Extensions.Configuration.CommandLine namespace.
        // * Environment variables - Use Microsoft.Extensions.Configuration.EnvironmentVariables namespace.
        // * Ini - data will be stored in ini format. Use Microsoft.Extensions.Configuration.Ini namespace.
        // * XML - data will be stored in XML format. Use Microsoft.Extensions.Configuration.Xml namespace.

        public IConfiguration AppConfiguration { get; set; }

        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder();

            //// Example #1
            //configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            //{
            //    { "name", "bill" },
            //    { "surname", "gates" }
            //});

            //this.AppConfiguration = configBuilder.Build();

            // Example #2
            configBuilder.SetBasePath(env.ContentRootPath);
            configBuilder.AddJsonFile("CustomJsonConfig.json");
            this.AppConfiguration = configBuilder.Build();

            // Example #3 - Retrieve config section
            // * GetSection(name) - gets IConfiguration object. It represents only one section.
            // * GetChildren() - gets all the subsections of a current configuration object. The returned result is IConfiguration.
            // * [key] - the direct call of a value.
            //this.AppConfiguration.GetSection("fakesection");
            //string value = this.AppConfiguration["fakeKey"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // For some reasons we have to add the line of code below If we want to be
            // able to retrieve values in controllers. It's essential.
            services.AddSingleton<IConfiguration>(this.AppConfiguration);
            services.AddSingleton<ILogger, FooLogger>();

            // Example #4
            services.Configure<LogOptions>(this.AppConfiguration.GetSection("logoptions"));
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