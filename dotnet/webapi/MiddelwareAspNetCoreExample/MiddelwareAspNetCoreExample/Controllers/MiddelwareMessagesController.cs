using Microsoft.AspNetCore.Mvc;
using MiddelwareAspNetCoreExample.Extensions;

namespace MiddelwareAspNetCoreExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MiddelwareMessagesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Messages = HttpContext.GetMiddlewareModel().Messages
            });
        }
    }
}