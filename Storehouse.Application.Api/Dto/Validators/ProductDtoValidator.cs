using FluentValidation;

namespace Storehouse.Application.Api.Dto.Validators
{
    /// <summary>
    /// Defines the fluent validation of a <see cref="ProductDto"/>
    /// </summary>
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        /// <summary>
        /// Default constructor that sets the validation rules
        /// </summary>
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .Length(2,128).WithMessage("{PropertyName} must be in a range of 2 and 128 characters");

            RuleFor(x => x.Stock)
                .GreaterThan(0).WithMessage("{PropertyName} cannot be negative");
        }
    }   
}
