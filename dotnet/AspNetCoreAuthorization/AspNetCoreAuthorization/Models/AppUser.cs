using Microsoft.AspNetCore.Identity;

namespace AspNetCoreAuthorization.Models
{
    public class AppUser : IdentityUser
    {
		public Cities City { get; set; }
		public QualificationLevels Qualifications { get; set; }
	}
}