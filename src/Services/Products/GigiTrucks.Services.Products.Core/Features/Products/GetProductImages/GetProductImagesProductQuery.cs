using GigiTrucks.Services.Products.Core.DTOs.Products;
using MediatR;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Products.GetProductImages;

public record GetProductImagesProductQuery(Guid ProductId) 
    : IRequest<OneOf<IEnumerable<ProductImageDto>, NotFound>>;