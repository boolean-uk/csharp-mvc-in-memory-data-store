using exercise.wwwapi.DTO;
using FluentValidation;

namespace exercise.wwwapi.Validators
{
    public class ProductValidation : AbstractValidator<ProductDto>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Category).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
        }
    }
}
