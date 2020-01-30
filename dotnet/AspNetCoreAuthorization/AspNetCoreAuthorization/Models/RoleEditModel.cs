﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace AspNetCoreAuthorization.Models
{
	public class RoleEditModel
    {
	    public IdentityRole Role { get; set; }

	    public IEnumerable<AppUser> Members { get; set; }

	    public IEnumerable<AppUser> NonMembers { get; set; }
    }
}
