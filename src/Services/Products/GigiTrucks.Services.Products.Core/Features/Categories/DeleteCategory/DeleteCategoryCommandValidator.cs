using FluentValidation;

namespace GigiTrucks.Services.Products.Core.Features.Categories.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();
    }
}