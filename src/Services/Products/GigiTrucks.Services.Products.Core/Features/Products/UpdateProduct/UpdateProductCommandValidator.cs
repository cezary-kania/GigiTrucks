using FluentValidation;

namespace GigiTrucks.Services.Products.Core.Features.Products.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty();
        
        RuleFor(c => c.CategoryId)
            .NotEmpty();
        
        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(c => c.Description)
            .MaximumLength(4000);
    }
}