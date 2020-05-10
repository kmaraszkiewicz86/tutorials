using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace GradeBook.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes
                .FirstOrDefault(t => t.MediaType == "application/xml");

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            if (appXmlType != null) config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            Startup.Bootstrapper(config);
        }
    }
}
