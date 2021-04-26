using Api.Utils;
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
            var commandsConnectionString = new CommandsConnectionString(Configuration["ConnectionString"]);
            var queriesConnectionString = new QueriesConnectionString(Configuration["QueriesConnectionString"]);

            services.AddSingleton(config);
            services.AddSingleton(commandsConnectionString);
            services.AddSingleton(queriesConnectionString);
            services.AddSingleton(new SessionFactory(commandsConnectionString));
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