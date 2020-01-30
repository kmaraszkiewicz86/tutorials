﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreAuthorization.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreAuthorization.Controllers
{
	[Authorize]
    public class AccountController : Controller
	{
		private UserManager<AppUser> _userManager;
		private SignInManager<AppUser> _signInManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[AllowAnonymous]
	    public IActionResult Login(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}

	    [HttpPost]
	    [AllowAnonymous]
	    [ValidateAntiForgeryToken]
	    public async Task<IActionResult> Login(LoginModel details,
		    string returnUrl)
	    {
		    if (ModelState.IsValid)
		    {
			    AppUser user = await _userManager.FindByEmailAsync(details.Email);
			    if (user != null)
			    {
				    await _signInManager.SignOutAsync();
				     Microsoft.AspNetCore.Identity.SignInResult result =
					    await _signInManager.PasswordSignInAsync(
						    user, details.Password, false, false);

				    if (result.Succeeded)
				    {
					    return Redirect(returnUrl ?? "/");
				    }
			    }
			    else
			    {
				    ModelState.AddModelError(nameof(LoginModel.Email),
					    "Invalid user or password");
			    }
		    }
		    return View(details);
	    }

		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public async Task<IActionResult> AccessDenied()
		{
			return View();
		}
    }
}