using System;
using FunWithAspNetCoreConfiguration.Infrastructure;
using FunWithAspNetCoreConfiguration.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FunWithAspNetCoreConfiguration.Controllers
{
    public class RegionController : BaseController
    {
        private readonly IConfiguration configuration;
        private readonly LogOptions options;
        private readonly ILogger logger;

        public RegionController(IConfiguration configuration, IOptions<LogOptions> options, ILogger logger)
        {
            this.configuration = configuration;
            this.options = options.Value;
            this.logger = logger;
        }

        // TODO: It's always better to have an asynchronous version of a method.
        public IActionResult Get(string regionName)
        {
            try
            {
                var section = this.configuration.GetSection("regionsection");
                var regionSection = section.GetSection(regionName);
                var number = regionSection.GetSection("number");
                var code = regionSection.GetSection("code");

                return Ok(new
                {
                    RegionName = regionName,
                    RegionNumber = number.Value,
                    RegionCode = code.Value
                });
            }
            catch (Exception ex)
            {
                this.logger.Log($"{this.options.Template} {ex.ToString()}");
                throw;
            }
        }
    }
}