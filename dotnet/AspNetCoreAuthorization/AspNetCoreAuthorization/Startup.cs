using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Infrastucture;
using AspNetCoreAuthorization.Models;
using AspNetCoreAuthorization.Models.InitalData;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreAuthorization
{
	public class Startup
	{
		private IConfigurationRoot _configuration;

		private IConfigurationRoot _usersInitialDataConfiguration;

		public Startup(IHostingEnvironment env)
		{
			_configuration = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json")
				.Build();

			_usersInitialDataConfiguration = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("usersinitialdata.json")
				.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddTransient<IPasswordValidator<AppUser>,
			//	CustomPasswordValidator>();

			services.AddDbContext<AppIdentityDbContext>(options =>
				options.UseSqlServer(
					_configuration["Data:StoreIdentity:ConnectionStrings:DefaultConnection"]));

			services.AddIdentity<AppUser, IdentityRole>(opts =>
				{
					opts.User.RequireUniqueEmail = true;
					//opts.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz0123456789";

					opts.Password.RequiredLength = 3;
					opts.Password.RequireNonAlphanumeric = false;
					opts.Password.RequireLowercase = false;
					opts.Password.RequireUppercase = false;
					opts.Password.RequireDigit = false;

				})
				.AddEntityFrameworkStores<AppIdentityDbContext>();

			services.ConfigureApplicationCookie(opts =>
			{
				opts.LoginPath = "/Account/Login";
				//opts.AccessDeniedPath = "/Account/AccessDenied";
			});

			//services.AddSingleton<IClaimsTransformation, LocationClaimsProvider>();
			//services.AddSingleton<IUserAccountInitializer, UserAccountInitializer>();

			services.AddMvc();

			AppIdentityDbContext.CreateUserAccounts(services.BuildServiceProvider(),
				_usersInitialDataConfiguration).Wait();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseStatusCodePages();
			app.UseDeveloperExceptionPage();
			app.UseStaticFiles();
			app.UseAuthentication();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
