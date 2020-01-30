namespace AspNetCoreAuthorization.Models.InitalData
{
    public class UserInitialData
    {
	    public string Name { get; set; }

	    public string Email { get; set; }

	    public string Password { get; set; }

	    public string[] Roles { get; set; }
    }
}
