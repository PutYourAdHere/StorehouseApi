using Microsoft.AspNetCore.Mvc;

namespace Application.Crosscutting.Filters.ModelValidatorFilters
{
    /// <summary>
    /// Generic model validator attribute for all controllers
    /// <code>
    ///    services.AddMvc(options =>
    ///    {
    ///         options.Filters.Add<ModelValidatorAttribute>();
    ///    });
    /// </code>
    /// </summary>
    public class ModelValidatorAttribute : TypeFilterAttribute
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public ModelValidatorAttribute() : base(typeof(ModelValidatorFilter)) { }
    }
}
