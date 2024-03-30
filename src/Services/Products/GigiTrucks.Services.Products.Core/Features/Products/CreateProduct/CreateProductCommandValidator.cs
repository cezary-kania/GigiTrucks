using FluentValidation;

namespace GigiTrucks.Services.Products.Core.Features.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.CategoryId)
            .NotEmpty();
        
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(c => c.Description)
            .MaximumLength(4000);
    }
}