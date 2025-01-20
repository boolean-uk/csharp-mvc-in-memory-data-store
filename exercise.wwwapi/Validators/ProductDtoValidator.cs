using exercise.wwwapi.Dto;
using FluentValidation;

namespace exercise.wwwapi.Validators
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator() {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Category).NotEmpty().MaximumLength(50);
        }
    }
}
