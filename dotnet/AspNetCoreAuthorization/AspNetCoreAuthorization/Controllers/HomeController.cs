using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAuthorization.Controllers
{
	public class HomeController : Controller
	{
		private UserManager<AppUser> _userManager;

		public Task<AppUser> CurrentUser =>
			_userManager.FindByNameAsync(HttpContext.User?.Identity?.Name ?? string.Empty);

		public HomeController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		/// <summary>
		/// Indexes this instance.
		/// </summary>
		/// <returns></returns>
		public IActionResult Index() =>
			View(GetData(nameof(Index)));

		[Authorize(Roles = "Admin")]
		public IActionResult OtherAction() => View("Index", GetData(nameof(OtherAction)));

		[Authorize]
		public async Task<IActionResult> UserProps() =>
			View(await CurrentUser);

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> UserProps([Required] Cities city,
			[Required] QualificationLevels qualifications)
		{
			if (ModelState.IsValid)
			{
				AppUser user = await CurrentUser;
				user.City = city;
				user.Qualifications = qualifications;
				await _userManager.UpdateAsync(user);
				return RedirectToAction("Index");
			}

			return View(await CurrentUser);
		}

		private Dictionary<string, object> GetData(string actionName) => 
			new Dictionary<string, object>
			{
				["Action"] = actionName,
				["User"] = HttpContext.User.Identity.Name,
				["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
				["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
				["In User Role"] = HttpContext.User.IsInRole("User"),
				["City"] = CurrentUser?.Result?.City ?? Cities.None,
				["Qualification"] = CurrentUser?.Result?.Qualifications ?? QualificationLevels.None
			};
	}
}