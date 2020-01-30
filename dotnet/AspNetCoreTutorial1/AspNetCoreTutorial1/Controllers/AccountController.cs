using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AspNetCoreTutorial1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTutorial1.Controllers
{
	[Produces("application/json")]
	[Route("api/Account")]
	public class AccountController : Controller
	{
		private UserManager<AppUser> _userManager;

		public AccountController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public IActionResult Get() =>
			Ok(_userManager.Users);

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new AppUser()
				{
					Email = model.Email,
					UserName = model.Name
				};

				var identityResult = await _userManager.CreateAsync(user, model.Password);

				if (identityResult.Succeeded)
				{
					return Ok(new { Id = user.Id });
				}

				AddModelError(identityResult);
			}

			return BadRequest(ModelState);
		}

		[HttpPost("{userName}/{roleName}")]
		public async Task<IActionResult> AddUserToRole([Required] string userName, [Required] string roleName)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByNameAsync(userName);

				if (user == null)
				{
					return NotFound($"User {userName} not found");
				}

				var result = await _userManager.AddToRoleAsync(user, roleName);

				if (result.Succeeded)
				{
					return Ok();
				}

				AddModelError(result);
			}

			return BadRequest(ModelState);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove([Required] string id)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(id);
				if (user == null)
				{
					return NotFound("User not found");
				}

				var identityResult = await _userManager.DeleteAsync(user);

				if (identityResult.Succeeded)
				{
					return Ok();
				}

				AddModelError(identityResult);
			}

			return BadRequest(ModelState);
		}

		private void AddModelError(IdentityResult identityResult)
		{
			foreach (var identityResultError in identityResult.Errors)
			{
				ModelState.AddModelError("", identityResultError.Description);
			}
		}
	}
}