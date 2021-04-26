using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreTutorial1.Models
{
    public class ComponentB : IComponent
    {
	    public string Name { get; set; } = nameof(ComponentB);

	    public async Task DoWork(HttpContext context)
	    {
		    await context.Response.WriteAsync("Testowanie");
	    }
    }
}
