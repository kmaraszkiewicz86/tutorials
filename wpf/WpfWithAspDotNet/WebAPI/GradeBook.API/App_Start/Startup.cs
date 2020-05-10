using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.Extensions.DependencyInjection;
using GradeBook.Core.Services.Implementations;
using GradeBook.Core.Services.Interfaces;

namespace GradeBook.API
{
    public class Startup
    {
        public static void Bootstrapper(HttpConfiguration config)
        {
            var provider = Configuration();
            var resolver = new DefaultDependencyResolver(provider);

            config.DependencyResolver = resolver;
        }

        private static IServiceProvider Configuration()
        {
            var services = new ServiceCollection();

            services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                .Where(t => typeof(IHttpController).IsAssignableFrom(t)
                            || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));

            services.AddScoped<IStudentDbService, StudentDbService>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }
    }
}