using FluentValidation;

namespace Storehouse.Application.Api.Dto.Validators
{
    /// <summary>
    /// Defines the fluent validation of a <see cref="StockChangeDto"/>
    /// </summary>
    public class StockChangeDtoValidator : AbstractValidator<StockChangeDto>
    {
        /// <summary>
        /// Default constructor that sets the validation rules
        /// </summary>
        public StockChangeDtoValidator()
        {
            RuleFor(x => x.Stock)
                .GreaterThan(0).WithMessage("{PropertyName} cannot be negative");
        }
    }   
}
