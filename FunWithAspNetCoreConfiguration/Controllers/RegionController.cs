using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FunWithAspNetCoreConfiguration.Controllers
{
    public class RegionController : BaseController
    {
        private readonly IConfiguration configuration;

        public RegionController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // TODO: It's always better to have an asynchronous version of a method.
        public IActionResult Get(string regionName)
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
    }
}