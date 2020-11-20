using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Crosscutting.Filters.ModelValidatorFilters
{
    /// <summary>
    /// Filter for returning a result if the given model to a controller does not pass validation
    /// </summary>
    internal class ModelValidatorFilter : IActionFilter
    {
        /// <summary>
        /// Check the model before execution
        /// </summary>
        /// <param name="context">executing context</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

        /// <summary>
        /// Check the model after execution
        /// </summary>
        /// <param name="context">executed context</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {}
    }
}
