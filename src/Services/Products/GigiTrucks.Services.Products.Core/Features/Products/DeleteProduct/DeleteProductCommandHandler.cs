using GigiTrucks.Services.Products.Core.DAL.EF;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace GigiTrucks.Services.Products.Core.Features.Products.DeleteProduct;

internal sealed class DeleteProductCommandHandler(ProductsDbContext dbContext) 
    : IRequestHandler<DeleteProductCommand, OneOf<Success,NotFound>>
{
    public async Task<OneOf<Success, NotFound>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        if (product is null)
        {
            return new NotFound();
        }
        
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return new Success();
    }
}