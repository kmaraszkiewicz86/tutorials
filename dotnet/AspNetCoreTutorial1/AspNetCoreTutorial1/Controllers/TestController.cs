using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTutorial1.Controllers
{
	[Route("api/Test")]
    public class TestController : Controller
    {
		[HttpGet]
		public string[] Get() =>
			new string[] {"a", "b"};
    }
}
