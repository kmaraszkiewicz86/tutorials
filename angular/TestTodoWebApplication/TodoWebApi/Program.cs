﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace TodoWebApi
{
	/// <summary>
	/// Program class.
	/// </summary>
	public class Program
    {
		/// <summary>
		/// Builds the web host.
		/// </summary>
		/// <param name="args">The arguments.</param>
		/// <returns></returns>
		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
			.UseStartup<Startup>()
			.Build();

		/// <summary>
		/// Mains the specified arguments.
		/// </summary>
		/// <param name="args">The arguments.</param>
		public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
    }
}