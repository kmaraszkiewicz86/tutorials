using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTutorial1.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTutorial1.Models
{
	[ModelBinder(typeof(TestModelBinder))]
    public class TestModelBinderModel
    {
	    public string PartOne { get; set; }

	    public string PartTwo { get; set; }
    }
}
