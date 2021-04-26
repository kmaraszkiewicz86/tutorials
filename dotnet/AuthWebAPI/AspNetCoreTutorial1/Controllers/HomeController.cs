using AspNetCoreTutorial1.ConfigurationModels;
using AspNetCoreTutorial1.Models;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTutorial1.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspNetCoreTutorial1.Controllers
{
	[Route("api/home")]
    public class HomeController : Controller
	{
		private IOptionsSnapshot<ConfigurationModel> _options;
		private readonly ILogger<HomeController> _logger;

		public HomeController(IOptionsSnapshot<ConfigurationModel> options, ILogger<HomeController> logger)
		{
			_options = options;
			_logger = logger;
		}

		[HttpGet]
		[TimestampFilter]
        public IActionResult Get([FromHeader(Name = "Content-Type")] string contentType, string timestamp)
        {
	        using (_logger.BeginScope("Scope"))
	        {
				_logger.LogInformation("LogInformation");
				_logger.LogWarning("LogWarning");
				_logger.LogError("LogError");
			}
	        return Ok(new string[] {contentType, timestamp});
        }

		[HttpGet("GetSettings")]
		public IActionResult GetSettings()
		{
			return Ok(_options.Value);
		}

	    [HttpPost]
	    public IActionResult Get([FromBody] TestModelBinderModel model)
	    {
		    return Ok(model);
	    }

		[HttpPost("ValidateWithAttribute")]
		public IActionResult ValidateWithAttribute ([FromBody] NameModel model)

	    {
		    if (ModelState.IsValid)
		    {
			    return Ok(model);
		    }

		    return BadRequest(ModelState);
	    }

		[HttpPost("ValidateWithInnerValidation")]
	    public IActionResult ValidateWithInnerValidation([FromBody] NameWithInternalValidationModel model)
	    {
		    if (ModelState.IsValid)
		    {
			    return Ok(model);
		    }

		    return BadRequest(ModelState);
	    }
    }
}