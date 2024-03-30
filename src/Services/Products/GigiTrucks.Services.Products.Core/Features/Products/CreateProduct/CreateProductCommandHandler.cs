using FluentValidation;
using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    ProductsDbContext dbContext,
    IValidator<CreateProductCommand> validator) : IRequestHandler<CreateProductCommand, OneOf<Success, Error<string>>>
{
    public async Task<OneOf<Success, Error<string>>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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
            return new Error<string>("Category not found.");
        }
        
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Category = category
        };
        
        await dbContext.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new Success();
    }
}