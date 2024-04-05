using FluentValidation;
using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace GigiTrucks.Services.Products.Core.Features.Categories.CreateCategory;

internal sealed class CreateCategoryCommandHandler(
    ProductsDbContext dbContext,
    IValidator<CreateCategoryCommand> validator) 
    : IRequestHandler<CreateCategoryCommand, OneOf<Success,Error<string>>>
{
    public async Task<OneOf<Success,Error<string>>> Handle(
        CreateCategoryCommand request, 
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return new Error<string>(validationResult.Errors.ToString()!);
        }
        
        if (await dbContext.Categories.AnyAsync(x => x.Name == request.Name, cancellationToken))
        {
            return new Error<string>("Category already exists.");
        }

        if (request.ParentCategoryId is not null)
        {
            return await CreateSubCategoryAsync(request, cancellationToken);
        }
        return await CreateNewCategoryAsync(request, cancellationToken);
    }

    private async Task<OneOf<Success, Error<string>>> CreateNewCategoryAsync(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
        await dbContext.Categories.AddAsync(category, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new Success();
    }

    private async Task<OneOf<Success, Error<string>>> CreateSubCategoryAsync(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var parentCategory = await dbContext.Categories
            .FirstOrDefaultAsync(x => x.Id == request.ParentCategoryId, cancellationToken);

        if (parentCategory is null)
        {
            return new Error<string>("Parent category does not exist.");
        }
        
        var newCategory = new Category
        {
            Name = request.Name
        };
        parentCategory.SubCategories.Add(newCategory);
        
        dbContext.Categories.Update(parentCategory);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new Success();
    }
}