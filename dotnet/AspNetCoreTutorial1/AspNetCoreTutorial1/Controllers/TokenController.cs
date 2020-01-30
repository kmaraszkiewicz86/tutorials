using AspNetCoreTutorial1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTutorial1.Controllers
{
	[Route("api/[controller]")]
    public class TokenController : Controller
    {
	    private readonly IConfiguration _configuration;
	    private UserManager<AppUser> _userManager;
	    private SignInManager<AppUser> _signInManager;
	    private RoleManager<IdentityRole> _roleManager;

	    public TokenController(IConfiguration configuration, UserManager<AppUser> userManager, 
		    SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
	    {
		    _configuration = configuration;
		    _userManager = userManager;
		    _signInManager = signInManager;
		    _roleManager = roleManager;
	    }

	    [AllowAnonymous]
	    [HttpPost]
	    public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
	    {
		    IActionResult response = Unauthorized();
		    var user = await Authenticate(model);

		    if (user != null)
		    {
			    var tokenString = await BuildToken(user);
			    response = Ok(new {token = tokenString});
		    }

		    return response;
	    }

	    private async Task<string> BuildToken(AppUser user)
	    {
		    var claims = new List<Claim>
		    {
			    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
			    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			    new Claim(ClaimTypes.NameIdentifier, user.Id)
		    };

		    var userRoles = await _userManager.GetRolesAsync(user);
		    var userClaims = await _userManager.GetClaimsAsync(user);
			claims.AddRange(userClaims);

		    foreach (var userRole in userRoles)
		    {
			    claims.Add(new Claim(ClaimTypes.Role, userRole));
			    var role = await _roleManager.FindByNameAsync(userRole);
			    if (role != null)
			    {
				    var roleClaims = await _roleManager.GetClaimsAsync(role);
				    foreach (var roleClaim in roleClaims)
				    {
					    claims.Add(roleClaim);
				    }
			    }
		    }

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
			    _configuration["Jwt:Issuer"],
				claims,
			    expires: DateTime.Now.AddMinutes(30),
			    signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
	    }

	    private async Task<AppUser> Authenticate(LoginModel model)
	    {
		    AppUser user = await _userManager.FindByNameAsync(model.Username);

		    if (user != null)
		    {
			    await _signInManager.SignOutAsync();
			    Microsoft.AspNetCore.Identity.SignInResult result =
				    await _signInManager.PasswordSignInAsync(
					    user, model.Password, false, false);

			    if (!result.Succeeded)
			    {
				    return null;
			    }
		    }
		    else
		    {
			    ModelState.AddModelError(nameof(LoginModel.Username),
				    "Invalid user or password");
		    }

		    return user;
	    }
    }
}