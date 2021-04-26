using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreTutorial1.Filters
{
    public class TimestampFilterAttribute : Attribute, IActionFilter, IAsyncActionFilter
    {
	    /// <summary>
	    /// Called before the action executes, after model binding is complete.
	    /// </summary>
	    /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
	    public void OnActionExecuting(ActionExecutingContext context)
	    {
		    context.ActionDescriptor.RouteValues["timestamp"] = DateTime.Now.ToString(CultureInfo.InvariantCulture);
	    }

	    /// <summary>
	    /// Called after the action executes, before the action result.
	    /// </summary>
	    /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
	    public void OnActionExecuted(ActionExecutedContext context)
	    {
		    var ts = DateTime.Parse(context.ActionDescriptor.RouteValues["timestamp"], CultureInfo.InvariantCulture).AddHours(1)
			    .ToString(CultureInfo.InvariantCulture);

		    context.HttpContext.Response.Headers["X-EXPIRY-TIMESTAMP"] = ts;
	    }

	    /// <summary>
	    /// Called asynchronously before the action, after model binding is complete.
	    /// </summary>
	    /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
	    /// <param name="next">
	    /// The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate" />. Invoked to execute the next action filter or the action itself.
	    /// </param>
	    /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that on completion indicates the filter has executed.</returns>
	    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	    {
		    OnActionExecuting(context);
		    var result = await next();
			OnActionExecuted(result);
	    }
    }
}