using System;
using FunWithAspNetCoreConfiguration.Infrastructure;
using FunWithAspNetCoreConfiguration.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FunWithAspNetCoreConfiguration.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        private readonly LogOptions options;

        public PersonController(IConfiguration configuration, IOptions<LogOptions> options, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.options = options.Value;
        }

        // TODO: It's always better to have an asynchronous version of a method.
        public IActionResult Get()
        {
            try
            {
                var fullName = $"{this.configuration["name"]} {this.configuration["surname"]}";
                return Ok(fullName);
            }
            catch (Exception ex)
            {
                this.logger.Log($"{this.options.Template} {ex.ToString()}");
                throw;
            }
        }
    }
}