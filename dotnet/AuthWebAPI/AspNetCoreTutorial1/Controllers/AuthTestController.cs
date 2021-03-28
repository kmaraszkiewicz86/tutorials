using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreTutorial1.Controllers
{
    [Route("api/AuthTest")]
    public class AuthTestController : Controller
	{
		public IActionResult Get() =>
			Ok(new {result = "ok"});

		[Authorize]
		[HttpGet("all")]
		public IActionResult AllTest() =>
			GetUser();

		[Authorize(Policy = "Admin")]
		[HttpGet("admin")]
	    public IActionResult AdminTest() =>
		    GetUser();

		[Authorize(Policy = "User")]
		[HttpGet("user")]
		public IActionResult UserTest() =>
		    GetUser();

		private IActionResult GetUser()
		{
			return Ok(new { result = "ok" });
		}
		    
    }
}
