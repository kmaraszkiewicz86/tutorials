using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreTutorial1.Helpers
{
    public class TestHostedService : IHostedService
    {
	    private readonly IHostingEnvironment _hostingEnvironment;

	    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
	    public TestHostedService(IHostingEnvironment hostingEnvironment)
	    {
		    _hostingEnvironment = hostingEnvironment;
	    }

	    /// <summary>
	    /// Triggered when the application host is ready to start the service.
	    /// </summary>
	    public async Task StartAsync(CancellationToken cancellationToken)
	    {
		    var file = $@"{_hostingEnvironment.ContentRootPath}\wwwroot\test.json";
		    var content = $"<root><test-date>{DateTime.Now:yyyy-MM-dd HH:mm:ss}</test-date></root>";
			
		    try
		    {
			    await File.WriteAllTextAsync(file, content, cancellationToken);
			}
		    catch (Exception e)
		    {
			    Console.WriteLine(e);
			    throw;
		    }

		    Thread.Sleep(10000);
	    }

	    /// <summary>
	    /// Triggered when the application host is performing a graceful shutdown.
	    /// </summary>
	    public Task StopAsync(CancellationToken cancellationToken)
	    {
			return Task.FromResult(0);
		}
    }
}
