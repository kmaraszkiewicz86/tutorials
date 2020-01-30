using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTutorial1.Models
{
    public class DeleteModel
    {
		[Required]
	    public string Id { get; set; }
    }
}
