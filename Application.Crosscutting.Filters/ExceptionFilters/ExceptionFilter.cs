using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Application.Crosscutting.Filters.ExceptionFilters
{
    /// <summary>
    /// Allows to manage unhandled exceptions and give the same response to the clients
    /// </summary>
    internal class ExceptionFilter : IExceptionFilter
    {

        /// <summary>
        /// Logger provider
        /// </summary>
        private readonly ILogger<ExceptionFilter> _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Captures the exception, log the error and change the result of the response.
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogError($"Message: {context.Exception.Message}");
            _logger.LogError($"Source: {context.Exception.Source}");
            _logger.LogError($"StackTrace: {context.Exception.StackTrace}");

            context.HttpContext.Response.StatusCode = 500;
            context.ExceptionHandled = true;

            context.Result = new ObjectResult(new { Error = "An unhandled error occurred" });
        }
    }
}
