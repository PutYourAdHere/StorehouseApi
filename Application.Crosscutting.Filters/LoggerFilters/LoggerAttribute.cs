using Microsoft.AspNetCore.Mvc;

namespace Application.Crosscutting.Filters.LoggerFilters
{
    /// <summary>
    /// Logging attribute for all Controllers
    /// <code>
    ///    services.AddMvc(options =>
    ///    {
    ///         options.Filters.Add<LoggerAttribute>();
    ///    });
    /// </code>
    /// </summary>
    public class LoggerAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public LoggerAttribute()
            : base(typeof(LoggerFilter))
        {
        }
        
    }
}
