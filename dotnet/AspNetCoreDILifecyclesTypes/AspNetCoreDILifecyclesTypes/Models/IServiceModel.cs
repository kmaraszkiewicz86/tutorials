using AspNetCoreDILifecyclesTypes.Core;

namespace AspNetCoreDILifecyclesTypes.Models
{
	public interface IServiceModel
	{
		ISingletonService SingletonService { get; }

		ITransientService TransientService { get; }

		IScopedService ScopedService { get; }
	}
}