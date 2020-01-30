using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreAuthorization.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RoleAdminController : Controller
	{
		private RoleManager<IdentityRole> _roleManager;
		private UserManager<AppUser> _userManager;

		public RoleAdminController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public IActionResult Index() => View(_roleManager.Roles);

		public async Task<IActionResult> Edit(string id)
		{
			IdentityRole role = await _roleManager.FindByIdAsync(id);
			var members = new List<AppUser>();
			var nonMembers = new List<AppUser>();
			foreach (var appUser in _userManager.Users)
			{
				var list = await _userManager.IsInRoleAsync(appUser, role.Name)
					? members
					: nonMembers;
				list.Add(appUser);

			}

			return View(new RoleEditModel
			{
				Role = role,
				Members = members,
				NonMembers = nonMembers
			});
		}

		[HttpPost]
		public async Task<IActionResult> Edit(RoleModificationModel model)
		{
			IdentityResult result;

			if (ModelState.IsValid)
			{
				foreach (var userId in model.IdsToAdd ?? new string[] { })
				{
					AppUser user = await _userManager.FindByIdAsync(userId);
					if (user != null)
					{
						result = await _userManager.AddToRoleAsync(user,
							model.RoleName);
						if (!result.Succeeded)
						{
							AddErrorsFromResult(result);
						}
					}
				}

				foreach (var userId in model.IdsToDelete ?? new string[] { })
				{
					AppUser appUser = await _userManager.FindByIdAsync(userId);
					if (appUser != null)
					{
						result = await _userManager.RemoveFromRoleAsync(appUser, model.RoleName);
						if (!result.Succeeded)
						{
							AddErrorsFromResult(result);
						}
					}
				}
			}

			if (ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return await Edit(model.RoleId);
			}
		}

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create([Required] string name)
		{
			if (ModelState.IsValid)
			{
				IdentityResult result =
					await _roleManager.CreateAsync(new IdentityRole(name));

				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					AddErrorsFromResult(result);
				}
			}

			return View(name);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			IdentityRole role =
				await _roleManager.FindByIdAsync(id);

			if (role != null)
			{
				IdentityResult result = await _roleManager.DeleteAsync(role);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					AddErrorsFromResult(result);
				}
			}
			else
			{
				ModelState.AddModelError("", "Role not found");
			}
			return View("Index", role);
		}

		private void AddErrorsFromResult(IdentityResult result)
		{
			foreach (IdentityError identityError in result.Errors)
			{
				ModelState.AddModelError("", identityError.Description);
			}
		}
	}
}