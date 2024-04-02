using FluentValidation;
using GigiTrucks.Services.Products.Core.DAL.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace GigiTrucks.Services.Products.Core.Features.Categories.DeleteCategory;

internal sealed class DeleteCategoryCommandHandler(
    ProductsDbContext dbContext,
    IValidator<DeleteCategoryCommand> validator)
    : IRequestHandler<DeleteCategoryCommand,OneOf<Success,NotFound,Error<string>>>
{
    public async Task<OneOf<Success,NotFound,Error<string>>> Handle(
        DeleteCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return new Error<string>(validationResult.Errors.ToString()!);
        }

        var category = await dbContext.Categories
            .Include(x => x.SubCategories)
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (category is null)
        {
            return new NotFound();
        }

        if (category.SubCategories.Count > 0)
        {
            return new Error<string>("Can not delete category with subcategories.");
        }
        
        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new Success();
    }
}