using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTutorial1.Models
{
    public class NameWithInternalValidationModel : IValidatableObject
    {
	    /// <summary>Determines whether the specified object is valid.</summary>
	    /// <param name="validationContext">The validation context.</param>
	    /// <returns>A collection that holds failed-validation information.</returns>
	    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
	    {
			var validationResults = new List<ValidationResult>();
		    if (Name[0] != Name?.ToUpper()[0])
		    {
			    validationResults.Add(new ValidationResult("Value must start with capital letter"));
		    }

		    return validationResults;
	    }

	    public string Name { get; set; }
    }
}
