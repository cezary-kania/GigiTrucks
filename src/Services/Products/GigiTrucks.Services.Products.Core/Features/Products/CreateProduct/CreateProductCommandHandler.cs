using FluentValidation;
using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Products.Core.Features.Products.CreateProduct;

internal sealed class CreateProductCommandHandler(
    ProductsDbContext dbContext,
    IValidator<CreateProductCommand> validator) : IRequestHandler<CreateProductCommand>
{
    public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        await validator.ValidateAsync(request, cancellationToken);
        
        var product = new Product
        {
            Id = request.ProductId,
            Name = request.Name,
            Description = request.Description,
        };
        
        await dbContext.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}