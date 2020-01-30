using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreAuthorization.Infrastucture
{
    public class CustomPasswordValidator : PasswordValidator<AppUser>
    {
	    public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
	    {
		    IdentityResult result = await base.ValidateAsync(manager,
			    user, password);

		    var errors = result.Succeeded
			    ? new List<IdentityError>()
			    : result.Errors.ToList();

		    if (password.ToLower().Contains(user.UserName.ToLower()))
		    {
				errors.Add(new IdentityError
				{
					Code = "PasswordContainsUserName",
					Description = "Password cannot contain username"
				});
		    }

		    if (password.Contains("12345"))
		    {
				errors.Add(new IdentityError
				{
					Code = "PasswordContatainSequence",
					Description = "Password cannot contain numeric sequnce"
				});
		    }

		    return !errors.Any()
			    ? IdentityResult.Success
			    : IdentityResult.Failed(errors.ToArray());
	    }
    }
}
