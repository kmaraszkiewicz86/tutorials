using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreAuthorization.Infrastucture
{
	[HtmlTargetElement("td", Attributes = "identity-role")]
	public class RoleUsersTagHelper : TagHelper
	{
		private UserManager<AppUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;

		public RoleUsersTagHelper(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HtmlAttributeName("identity-role")] public string Role { get; set; }

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			var names = new List<string>();
			IdentityRole role = await _roleManager.FindByIdAsync(Role);
			if (role != null)
			{
				foreach (AppUser appUser in _userManager.Users)
				{
					if (appUser != null 
					    && await _userManager.IsInRoleAsync(appUser, role.Name))
					{
						names.Add(appUser.UserName);
					}
				}
			}

			output.Content.SetContent(names.Any()
				? string.Join(",", names)
				: "No users");
		}
	}
}
