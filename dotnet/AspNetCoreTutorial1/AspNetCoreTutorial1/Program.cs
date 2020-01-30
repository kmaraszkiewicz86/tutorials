using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace AspNetCoreTutorial1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// NLog: setup the logger first to catch all errors
			var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
			try
			{
				logger.Debug("init main");
				BuildWebHost(args).Run();
			}
			catch (Exception ex)
			{
				//NLog: catch setup errors
				logger.Error(ex, "Stopped program because of exception");
				throw;
			}
			finally
			{
				// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
				NLog.LogManager.Shutdown();
			}
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.ConfigureLogging(logging =>
					{
						logging.ClearProviders();
						logging.SetMinimumLevel(LogLevel.Trace);
						logging.AddNLog();
					})
				.Build();
		//        new WebHostBuilder()
		//.UseEnvironment("Test")
		//.UseKestrel()
		//.UseContentRoot(Directory.GetCurrentDirectory())
		//.ConfigureAppConfiguration(config =>
		//          config.AddJsonFile("appSetting.json", true))
		//.ConfigureLogging(logging =>
		//	logging.AddConsole())
		//.UseIISIntegration()
		//.UseStartup<Startup>()
		//.Build();
	}
}
