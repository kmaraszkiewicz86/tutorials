using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTutorial1.Controllers
{
    [Produces("application/json")]
    [Route("api/TestAuth")]
    public class TestAuthController : Controller
    {
		[Authorize]
	    public IEnumerable<TestAuthModel> Get()
		{
			var user = HttpContext.User;

			var items = new List<TestAuthModel>
			{
				new TestAuthModel { Test1 = "1", Test2 = "1" },
				new TestAuthModel { Test1 = "2", Test2 = "2" }
			};

			if (user.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
			{
				items.ForEach(i => i.Age = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)?.Value);
			}

			return items;
		}
    }

	public class TestAuthModel
	{
		public string Test1 { get; set; }

		public string Test2 { get; set; }

		public string Age { get; set; }
	}
}