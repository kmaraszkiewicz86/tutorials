using AspNetCoreDILifecyclesTypes.Core;

namespace AspNetCoreDILifecyclesTypes.Models
{
	public class ServicesModel : IServiceModel
	{
		public ISingletonService SingletonService { get; }

		public ITransientService TransientService { get; }

		public IScopedService ScopedService { get; }

		public ServicesModel(ISingletonService singletonService, ITransientService transientService, IScopedService scopedService)
		{
			SingletonService = singletonService;
			TransientService = transientService;
			ScopedService = scopedService;
		}
	}
}