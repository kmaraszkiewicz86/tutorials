using AspNetCoreDILifecyclesTypes.Core;
using AspNetCoreDILifecyclesTypes.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDILifecyclesTypes.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISingletonService _singletonService;
		private readonly ITransientService _transientService;
		private readonly IScopedService _scopedService;
		private readonly IServiceModel _serviceModel;

		public HomeController(ISingletonService singletonService, ITransientService transientService, IScopedService scopedService, IServiceModel servicesModel)
		{
			_singletonService = singletonService;
			_transientService = transientService;
			_scopedService = scopedService;
			_serviceModel = servicesModel;
		}
		
		public IActionResult Index()
		{
			ViewBag.SingletonService = _singletonService;
			ViewBag.TransientService = _transientService;
			ViewBag.ScopedService = _scopedService;

			return View(_serviceModel);
		}
	}
}