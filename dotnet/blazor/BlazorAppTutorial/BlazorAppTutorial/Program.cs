using BlazorAppTutorial.Api.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorAppTutorial
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(
                client => client.BaseAddress = new Uri("https://localhost:5001/"));

            await builder.Build().RunAsync();
        }
    }
}
