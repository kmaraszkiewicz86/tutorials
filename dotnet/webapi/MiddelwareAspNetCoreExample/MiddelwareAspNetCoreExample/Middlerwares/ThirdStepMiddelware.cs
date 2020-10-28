using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MiddelwareAspNetCoreExample.Extensions;

namespace MiddelwareAspNetCoreExample.Middlerwares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ThirdStepMiddelware
    {
        private readonly RequestDelegate _next;

        public ThirdStepMiddelware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.AddNewMiddlewareMessage($"{nameof(ThirdStepMiddelware)}");
            return _next(httpContext);
        }
    }
}
