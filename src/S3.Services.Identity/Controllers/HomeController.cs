using Microsoft.AspNetCore.Mvc;
using S3.Common.Dispatchers;
using System.Threading.Tasks;

namespace S3.Services.Identity.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("Identity Service running...");

    }
}