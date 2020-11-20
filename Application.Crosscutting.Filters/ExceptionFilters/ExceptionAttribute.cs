using Microsoft.AspNetCore.Mvc;

namespace Application.Crosscutting.Filters.ExceptionFilters
{
    /// <summary>
    /// Allow to add a exception filter to all controllers
    /// </summary>
    public class ExceptionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ExceptionAttribute() : base(typeof(ExceptionFilter)) { }
    }
}
