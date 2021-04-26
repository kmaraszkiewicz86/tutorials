using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreTutorial1.Helpers
{
	public class TestMiddelware
	{
		private readonly RequestDelegate _next;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
		public TestMiddelware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			await context.Response.WriteAsync("Testowanie middelware");
			await _next(context);
		}
	}
}
