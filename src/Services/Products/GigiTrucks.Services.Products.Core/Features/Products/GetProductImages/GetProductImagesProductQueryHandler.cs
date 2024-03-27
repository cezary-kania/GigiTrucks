using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.DTOs.Products;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Products.GetProductImages;

internal sealed class GetProductImagesProductQueryHandler(ProductsDbContext dbContext) 
    : IRequestHandler<GetProductImagesProductQuery, OneOf<IEnumerable<ProductImageDto>, NotFound>>
{
    public async Task<OneOf<IEnumerable<ProductImageDto>, NotFound>> Handle(
        GetProductImagesProductQuery request, 
        CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        
        if (product is null)
        {
            return new NotFound();
        }
        
        return OneOf<IEnumerable<ProductImageDto>, NotFound>.FromT0(
            product.Images.Adapt<IEnumerable<ProductImageDto>>());
    }
}