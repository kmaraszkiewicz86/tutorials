using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Models.InitalData;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreAuthorization.Models
{
	/// <summary>
	/// AppIdentityDbContext class.
	/// </summary>
	public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
	    public AppIdentityDbContext(DbContextOptions options) : base(options)
	    {
	    }

	    public static async Task CreateUserAccounts(IServiceProvider serviceProvider,
		    IConfiguration configuration)
	    {
		    UserManager<AppUser> userManager =
			    serviceProvider.GetRequiredService<UserManager<AppUser>>();
		    RoleManager<IdentityRole> roleManager =
			    serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			var usersInitialDataCollection = new UsersInitialDataCollection();

		    for (var index = 0; ;index++)
		    {
				if (configuration[$"Users:{index}:Name"] == null)
					break;

			    var roles = new List<string>();

			    for (var i = 0; ;i++)
			    {
				    var roleName = configuration[$"Users:{index}:Roles:{i}"];
					if (roleName == null)
						break;

					roles.Add(roleName);
			    }

				usersInitialDataCollection.Users.Add(new UserInitialData
				{
					Name = configuration[$"Users:{index}:Name"],
					Email = configuration[$"Users:{index}:Email"],
					Password = configuration[$"Users:{index}:Password"],
					Roles = roles.ToArray()
				});
		    }

		    foreach (var userInitialData in usersInitialDataCollection.Users)
		    {
			    if (await userManager.FindByNameAsync(userInitialData.Name) == null)
			    {
				    foreach (var role in userInitialData.Roles)
				    {
						if (await roleManager.FindByNameAsync(role) == null)
						{
							await roleManager.CreateAsync(new IdentityRole(role));
						}
					}

				    AppUser user = new AppUser
				    {
					    UserName = userInitialData.Name,
					    Email = userInitialData.Email
				    };

				    IdentityResult result = await userManager.CreateAsync(user, userInitialData.Password);
				    if (result.Succeeded)
				    {
					    foreach (var role in userInitialData.Roles)
					    {
						    await userManager.AddToRoleAsync(user, role);
						}
				    }
			    }
			}
		}
    }
}
