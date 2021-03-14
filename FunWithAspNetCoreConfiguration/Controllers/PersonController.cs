using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FunWithAspNetCoreConfiguration.Controllers
{
    public class PersonController : BaseController
    {
        private readonly IConfiguration configuration;

        public PersonController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // TODO: It's always better to have an asynchronous version of a method.
        public IActionResult Get()
        {
            var fullName = $"{this.configuration["name"]} {this.configuration["surname"]}";
            return Ok(fullName);
        }
    }
}