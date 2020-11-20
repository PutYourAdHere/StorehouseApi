using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Crosscutting.Filters.LoggerFilters
{
    /// <summary>
    /// Log action filter for logging all the requests arguments before and after its execution. 
    /// </summary>
    internal class LoggerFilter : IActionFilter
    {

        /// <summary>
        /// The logger which will perform the activity
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initialize the action filter with a logger factory
        /// </summary>
        /// <param name="loggerFactory">The logger used for performing the activity</param>
        public LoggerFilter(ILoggerFactory loggerFactory)
        {
            
            _logger = loggerFactory.CreateLogger<LoggerAttribute>();
        }

        /// <summary>
        /// Method called before action execution
        /// </summary>
        /// <param name="context">the execution context</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments != null) _logger.LogDebug(JsonConvert.SerializeObject(context.ActionArguments));
        }

        /// <summary>
        /// Method called after action execution
        /// </summary>
        /// <param name="context">The executed context</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Result != null) _logger.LogDebug(JsonConvert.SerializeObject(context.Result));
        }
    }
}
