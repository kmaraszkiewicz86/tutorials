using AspNetCoreTutorial1.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ILocalComponent = AspNetCoreTutorial1.Models.IComponent;

namespace AspNetCoreTutorial1
{
    public class StartupTest
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddSingleton<ILocalComponent, ComponentB>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILocalComponent component)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			app.Use(async (context, next) =>
			{
				if (context.Request.Path == "/foo")
				{
					await context.Response.WriteAsync($"Welcome to Foo");
				}
				else
				{
					await next();
				}
			});

			app.Use(async (context, next) =>
			{
				if (context.Request.Path == "/bar")
				{
					await context.Response.WriteAsync($"Welcome to Bar");
				}
				else
				{
					await next();
				}
			});

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync($"Test envinronment");
            });
        }
    }
}
