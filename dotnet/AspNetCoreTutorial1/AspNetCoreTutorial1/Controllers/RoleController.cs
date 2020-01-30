using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AspNetCoreTutorial1.Controllers
{
	[Produces("application/json")]
    [Route("api/Role")]
    public class RoleController : Controller
    {
	    private RoleManager<IdentityRole> _roleManager;

	    public RoleController(RoleManager<IdentityRole> roleManager)
	    {
		    _roleManager = roleManager;
	    }

	    // GET: api/Role
		[HttpGet]
	    public IActionResult Get() =>
		    Ok(_roleManager.Roles);
        
        // POST: api/Role
        [HttpPost]
        public async Task<IActionResult> Create ([Required]string name)
        {
	        if (ModelState.IsValid)
	        {
		        var identityResult = await _roleManager.CreateAsync(new IdentityRole
		        {
					Name = name
		        });

		        if (identityResult.Succeeded)
		        {
			        return Ok();
		        }

				AddModelError(identityResult);
	        }

	        return BadRequest(ModelState);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Required] string id)
        {
	        if (ModelState.IsValid)
	        {
		        var role = await _roleManager.FindByIdAsync(id);

		        if (role == null)
		        {
			        return NotFound();
		        }

		        var result = await _roleManager.DeleteAsync(role);

		        if (result.Succeeded)
		        {
			        return Ok();
		        }

		        AddModelError(result);
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
