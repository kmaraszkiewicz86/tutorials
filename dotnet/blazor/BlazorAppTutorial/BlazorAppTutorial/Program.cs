using BlazorAppTutorial.Api.Services;
using BlazorAppTutorial.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace BlazorAppTutorial
{
    public class Program
    {
        private static readonly Dictionary<string, string> mainHosts = new Dictionary<string, string>
        {
            { "windowsIzabelaMaraszkiewiczIT", "http://localhost:5000" },
            { "other", "http://localhost:44340/" },
        };

        private static string _apiMainHostUrl = "http://localhost:5000";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(
                client => client.BaseAddress = new Uri(_apiMainHostUrl));

            builder.Services.AddHttpClient<ICountryDataService, CountryDataService>(
                client => client.BaseAddress = new Uri(_apiMainHostUrl));

            builder.Services.AddHttpClient<IJobCategoryDataService, JobCategoryDataService>(
                client => client.BaseAddress = new Uri(_apiMainHostUrl));

            await builder.Build().RunAsync();
        }
    }
}
