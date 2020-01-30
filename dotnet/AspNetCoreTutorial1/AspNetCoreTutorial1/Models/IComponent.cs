using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreTutorial1.Models
{
    public interface IComponent
    {
	    string Name { get; set; }

	    Task DoWork(HttpContext context);
    }
}
