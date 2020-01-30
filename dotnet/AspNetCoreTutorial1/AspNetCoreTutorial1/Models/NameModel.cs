using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTutorial1.Attributes;

namespace AspNetCoreTutorial1.Models
{
    public class NameModel
    {
		[StartWithCapitalLetter]
	    public string Name { get; set; }
    }
}
