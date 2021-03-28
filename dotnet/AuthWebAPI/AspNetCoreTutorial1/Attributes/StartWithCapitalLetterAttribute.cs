using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTutorial1.Attributes
{
    public class StartWithCapitalLetterAttribute : ValidationAttribute
    {
	    /// <summary>Validates the specified value with respect to the current validation attribute.</summary>
	    /// <param name="value">The value to validate.</param>
	    /// <param name="validationContext">The context information about the validation operation.</param>
	    /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"></see> class.</returns>
	    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	    {
		    if (value?.ToString()[0] != value?.ToString().ToUpper()[0])
		    {
				return new ValidationResult("Value must start with capital letter");
		    }

			return ValidationResult.Success;
	    }
    }
}
