using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTutorial1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Internal;

namespace AspNetCoreTutorial1.Helpers
{
    public class TestModelBinder : IModelBinder
    {
	    /// <summary>Attempts to bind a model.</summary>
	    /// <param name="bindingContext">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext" />.</param>
	    /// <returns>
	    /// <para>
	    /// A <see cref="T:System.Threading.Tasks.Task" /> which will complete when the model binding process completes.
	    /// </para>
	    /// <para>
	    /// If model binding was successful, the <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> should have
	    /// <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.IsModelSet" /> set to <c>true</c>.
	    /// </para>
	    /// <para>
	    /// A model binder that completes successfully should set <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext.Result" /> to
	    /// a value returned from <see cref="M:Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingResult.Success(System.Object)" />.
	    /// </para>
	    /// </returns>
	    public async Task BindModelAsync(ModelBindingContext bindingContext)
	    {
		    var value = bindingContext.ValueProvider.GetValue("value").FirstValue;
		    var values = value.Split(';');
			bindingContext.Result = ModelBindingResult.Success(new TestModelBinderModel
			{
				PartOne = values[0],
				PartTwo = values[1]
			});

		    await Task.FromResult(Task.CompletedTask);
	    }
    }
}
