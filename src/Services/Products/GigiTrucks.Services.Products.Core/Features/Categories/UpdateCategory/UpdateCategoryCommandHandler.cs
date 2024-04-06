using FluentValidation;
using GigiTrucks.Services.Products.Core.DAL.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Categories.UpdateCategory;

internal sealed class UpdateCategoryCommandHandler(
    ProductsDbContext dbContext,
    IValidator<UpdateCategoryCommand> validator) 
    : IRequestHandler<UpdateCategoryCommand, OneOf<Success,NotFound,Error<string>>>
{
    public async Task<OneOf<Success,NotFound,Error<string>>> Handle(
        UpdateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Error<string>(validationResult.Errors.ToString()!);
        }
        
        var category = await dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (category is null)
        {
            return new NotFound();
        }

        if (category.ParentCategoryId != request.ParentCategoryId)
        {
            var parentCategory = await dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == request.ParentCategoryId, cancellationToken);

            if (parentCategory is null)
            {
                return new Error<string>("Parent category does not exist.");
            }

            category.ParentCategory = parentCategory;
        }

        category.Name = request.Name;
        
        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new Success();
    }
}