using System.Collections.Generic;
using Api.Utils;
using Logic.Decorators;
using Logic.Dtos;
using Logic.Students.CommandHandlers;
using Logic.Students.Commands;
using Logic.Students.Queries;
using Logic.Students.QueryHandlers;
using Logic.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var config = new Config(3);

            services.AddSingleton(config);
            services.AddSingleton(new SessionFactory(Configuration["ConnectionString"]));
            services.AddSingleton<Messages>();
            services.AddHandlers();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();
            app.UseMvc();
        }
    }
}
