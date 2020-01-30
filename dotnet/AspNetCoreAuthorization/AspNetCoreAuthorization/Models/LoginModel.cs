using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAuthorization.Models
{
	public class LoginModel
    {
	    [Required]
	    [UIHint("email")]
	    public string Email { get; set; }

		[Required]
		[UIHint("password")]
		public string Password { get; set; }
    }
}