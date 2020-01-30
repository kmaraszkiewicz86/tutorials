using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAuthorization.Controllers
{
    public class ClaimsController : Controller
    {
	    public IActionResult Index() =>
		    View(User?.Claims);
    }
}