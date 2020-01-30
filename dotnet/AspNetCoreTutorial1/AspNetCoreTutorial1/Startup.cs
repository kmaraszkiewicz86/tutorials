using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using AspNetCoreTutorial1.ConfigurationModels;
using AspNetCoreTutorial1.Filters;
using AspNetCoreTutorial1.Helpers;
using AspNetCoreTutorial1.Models;
using AspNetCoreTutorial1.Models.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using ILocalComponent = AspNetCoreTutorial1.Models.IComponent;

namespace AspNetCoreTutorial1
{
	public class Startup
	{
		public IConfiguration Configuration { get; set; }

		private ILoggerFactory _loggerFactory;

		/// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
		public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile(config =>
				{
					config.Path = "appsettings.json";
					config.ReloadOnChange = true;
				});
			Configuration = builder.Build();
			_loggerFactory = loggerFactory;
			loggerFactory.CreateLogger("Startup.ctor").LogInformation("Logowanie z konstruktora");
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			_loggerFactory.CreateLogger("Startup.Configure").LogInformation("Logowanie z metody configure");

			//var builder = new ConfigurationBuilder()
			//	.SetBasePath(Directory.GetCurrentDirectory())
			//	.AddJsonFile("appsettings.json");

			//var config = new ConfigurationModel();
			//builder.Build().Bind(config);

			//Calling Configure not only works with the given ConfigurationRoot, but also works
			//quite well with configuration sections:
			//services.Configure<AwesomeOptions.BazOptions>(Configuration.
			//	GetSection("baz"));
			services.AddDbContext<TestDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<AppUser, IdentityRole>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.Password.RequiredLength = 3;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
			})
			.AddEntityFrameworkStores<TestDbContext>()
			.AddDefaultTokenProviders();

			services.Configure<ConfigurationModel>(Configuration);
			services.AddSingleton<ILocalComponent, ComponentB>();
			//services.AddSingleton<IHostedService, TestHostedService>();
			services.AddRouting();

			// ===== Add Jwt Authentication ========
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
			services
				.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

				})
				.AddJwtBearer(cfg =>
				{
					cfg.RequireHttpsMetadata = false;
					cfg.SaveToken = true;
					cfg.TokenValidationParameters = new TokenValidationParameters
					{
						ValidIssuer = Configuration["Jwt:Issuer"],
						ValidAudience = Configuration["Jwt:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
						ClockSkew = TimeSpan.Zero // remove delay of token when expire
					};
				});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("Admin", policy => 
					policy.RequireClaim(ClaimTypes.Role, "Admin"));

				options.AddPolicy("User", policy =>
					policy.RequireClaim(ClaimTypes.Role, "User"));
			});

			services.AddMvc(options =>
			{
				options.Filters.Add(typeof(TimestampFilterAttribute));

				options.ModelBinderProviders.Insert(0,
					new TestModelBinderProvider());
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILocalComponent component, ILogger<Startup> logger)
		{
			//app.Run(async (context) =>
			//{
			//	_loggerFactory.CreateLogger("Run").LogInformation("Run logowanie");
			//	logger.LogInformation(10, "testowanie 11111");
			//	await context.Response.WriteAsync("Testowanie");
			//});

			//      app.UseMiddleware<TestMiddelware>();
			//      app.UseRouter(builder =>
			//      {
			//       builder.MapRoute(string.Empty, context =>
			//       {
			//		var routeValues = new RouteValueDictionary
			//		{
			//			{ "number", 456 } 
			//		};

			//		var vpc = new VirtualPathContext(context, null, routeValues, "bar/{number:int}");
			//        var route = builder.Routes.Single(r => r.ToString().Equals(vpc.RouteName));
			//        var baseUrl = route.GetVirtualPath(vpc).VirtualPath;
			//        return context.Response.WriteAsync(baseUrl);
			//       });


			//       builder.MapGet("foo/{name}/{surname?}/{*aaa}", (request, response, routeData) => 
			//        response.WriteAsync($"Welcome to second route " +
			//                            $"{string.Join(";", routeData.Values.Select(data => $"{data.Key} => {data.Value}"))}"));

			//       builder.MapPost("bar/{number:int}", (context) =>
			//        context.Response.WriteAsync(
			//	        $"Welcome to post action" +
			//			$"{string.Join(";", context.GetRouteData().Values.Select(data => $"{data.Key} => {data.Value}"))}"));
			//});

			logger.LogError("test");

			app.UseStatusCodePages();
			app.UseDeveloperExceptionPage();
			app.UseAuthentication();
			app.UseMvc();
		}
	}
}

