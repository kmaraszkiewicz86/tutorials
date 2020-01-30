using Microsoft.Extensions.DependencyInjection;
using TodoDataModel.Core.DI;
using TodoDataModel.Core.Repository;

namespace TodoWebApi.AppStart
{
	/// <summary>
	/// class DependencyInjectonServices.
	/// </summary>
	public static class DependencyInjectonServices
	{
		/// <summary>
		/// Gets the services.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <returns></returns>
		public static IServiceCollection GetServices(this IServiceCollection services)
		{
			services.Add(new ServiceDescriptor(typeof(ITodoRepository), typeof(TodoRepository), ServiceLifetime.Transient));
			return services;
		}
	}
}
