using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace AspNetCoreAuthorization.Infrastucture
{
    public class LocationClaimsProvider : IClaimsTransformation
    {
	    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
	    {
		    if (principal != null
		        && !principal.HasClaim(c => c.Type == ClaimTypes.PostalCode))
		    {
			    if (principal.Identity is ClaimsIdentity identity 
			        && identity.IsAuthenticated && identity.Name != null)
			    {
				    if (identity.Name.ToLower() == "alice")
				    {
						identity.AddClaims(new []
						{
							CreateClaim(ClaimTypes.PostalCode, "62-002"),
							CreateClaim(ClaimTypes.StateOrProvince, "Suchy Las")
						});
				    }
				    else
				    {
					    identity.AddClaims(new []
					    {
							CreateClaim(ClaimTypes.PostalCode, "64-410"),
							CreateClaim(ClaimTypes.StateOrProvince, "Sieraków")
					    });
				    }
			    }
		    }

		    return Task.FromResult(principal);
	    }

	    private static Claim CreateClaim(string type, string value) =>
			new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");
    }
}