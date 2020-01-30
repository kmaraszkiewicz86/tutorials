using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreAuthorization.Models
{
    public interface IUserAccountInitializer
    {
	    Task CreateUserAccounts(IServiceProvider serviceProvider,
		    IConfiguration configuration);
    }
}
