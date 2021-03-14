using Microsoft.AspNetCore.Mvc;

namespace FunWithAspNetCoreConfiguration.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}