using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTutorial1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace AspNetCoreTutorial1.Helpers
{
    public class TestModelBinderProvider : IModelBinderProvider
    {
	    /// <summary>
	    /// Creates a <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" /> based on <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.
	    /// </summary>
	    /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext" />.</param>
	    /// <returns>An <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder" />.</returns>
	    public IModelBinder GetBinder(ModelBinderProviderContext context)
	    {
		    if (context.Metadata.ModelType == typeof(TestModelBinderModel))
		    {
				return new BinderTypeModelBinder(typeof(TestModelBinder));
		    }

		    return null;
	    }
    }
}
