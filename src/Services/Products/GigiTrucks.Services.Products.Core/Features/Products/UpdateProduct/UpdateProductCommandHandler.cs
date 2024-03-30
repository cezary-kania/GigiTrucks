using FluentValidation;
using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace GigiTrucks.Services.Products.Core.Features.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(
    ProductsDbContext dbContext,
    IValidator<UpdateProductCommand> validator) 
    : IRequestHandler<UpdateProductCommand, OneOf<Success,NotFound,Error<string>>>
{
    public async Task<OneOf<Success, NotFound, Error<string>>> Handle(
        UpdateProductCommand request, 
        CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return new Error<string>(validationResult.Errors.ToString()!);
        }

        var product = await dbContext.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == request.ProductId);

        if (product is null)
        {
            return new NotFound();
        }

        if (product.Category is null || product.Category?.Id != request.CategoryId)
        {
            var selectedCategory = await dbContext.Categories
                .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

            if (selectedCategory is null)
            {
                return new Error<string>("Category not found.");
            }

            product.Category = selectedCategory;
        }

        product.Name = request.Name;
        product.Description = request.Description;

        dbContext.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new Success();
    }
}