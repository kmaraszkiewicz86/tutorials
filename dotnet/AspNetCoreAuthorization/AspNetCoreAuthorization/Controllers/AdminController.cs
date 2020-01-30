using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAuthorization.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUserValidator<AppUser> _userValidator;
		private readonly IPasswordValidator<AppUser> _passwordValidator;
		private readonly IPasswordHasher<AppUser> _passwordHasher;

		public AdminController(UserManager<AppUser> userManager,
			IUserValidator<AppUser> userValidator,
			IPasswordValidator<AppUser> passwordValidator,
			IPasswordHasher<AppUser> passwordHasher)
		{
			_userManager = userManager;
			_userValidator = userValidator;
			_passwordValidator = passwordValidator;
			this._passwordHasher = passwordHasher;
		}

		public IActionResult Index() => View(_userManager.Users);

		public IActionResult Create() => View();

		[HttpPost]
		public async Task<IActionResult> Create(CreateModel model)
		{
			if (ModelState.IsValid)
			{
				AppUser user = new AppUser()
				{
					UserName = model.Name,
					Email = model.Email
				};

				IdentityResult result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					result = await _userManager.AddClaimsAsync(user, new[]
					{
						CreateClaim(ClaimTypes.PostalCode, "62-002"),
						CreateClaim(ClaimTypes.StateOrProvince, "Suchy Las")
					});

					if (result.Succeeded)
					{
						return RedirectToAction("Index");
					}

					return RedirectToAction("Index", model);
				}

				foreach (var identityError in result.Errors)
				{
					ModelState.AddModelError("", identityError.Description);
				}
			}

			return View(model);
		}

		public async Task<IActionResult> Edit(string id)
		{
			AppUser user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				return View(user);
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string id, string email, string password)
		{
			AppUser user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				user.Email = email;
				IdentityResult validEmail
					= await _userValidator.ValidateAsync(_userManager, user);
				if (!validEmail.Succeeded)
				{
					AddErrorsFromResult(validEmail);
				}

				IdentityResult validPass = null;
				if (!string.IsNullOrEmpty(password))
				{
					validPass = await _passwordValidator.ValidateAsync(_userManager, user, password);
					if (validPass.Succeeded)
					{
						user.PasswordHash = _passwordHasher.HashPassword(user, password);
					}
					else
					{
						AddErrorsFromResult(validPass);
					}
				}

				if ((validEmail.Succeeded && validPass == null)
					|| (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
				{
					IdentityResult result = await _userManager.UpdateAsync(user);
					if (result.Succeeded)
					{
						return RedirectToAction("Index");
					}

					AddErrorsFromResult(result);
				}
			}
			else
			{
				ModelState.AddModelError("", "User dont found");
			}

			return View(user);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			AppUser user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				IdentityResult result = await _userManager.DeleteAsync(user);
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
				ModelState.AddModelError("", "User not found");
			}

			return View("Index", _userManager.Users);
		}

		private void AddErrorsFromResult(IdentityResult result)
		{
			foreach (IdentityError err in result.Errors)
			{
				ModelState.AddModelError("", err.Description);
			}
		}

		private static Claim CreateClaim(string type, string value) =>
			new Claim(type, value, ClaimValueTypes.String, "LocalClaims");
	}
}