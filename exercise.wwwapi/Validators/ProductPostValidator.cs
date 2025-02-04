using exercise.wwwapi.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace exercise.wwwapi.Validators
{
    public class ProductPostValidator : AbstractValidator<Product>
    {
        public ProductPostValidator() 
        {
            RuleFor(p => p.name).NotEmpty().MaximumLength(50);
            RuleFor(p => p.category).NotEmpty().MaximumLength(50);
            RuleFor(p => p.price).NotEmpty().InclusiveBetween(0, 2000);
        }
    }
}
