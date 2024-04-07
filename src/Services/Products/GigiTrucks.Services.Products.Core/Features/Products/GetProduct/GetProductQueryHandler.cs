using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.DTOs.Products;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace GigiTrucks.Services.Products.Core.Features.Products.GetProduct;

internal sealed class GetProductQueryHandler(ProductsDbContext dbContext) 
    : IRequestHandler<GetProductQuery, OneOf<ProductDto,NotFound>>
{
    public async Task<OneOf<ProductDto,NotFound>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .Include(x => x.Images)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
        
        return product is not null ? product.Adapt<ProductDto>() : new NotFound();
    }
}