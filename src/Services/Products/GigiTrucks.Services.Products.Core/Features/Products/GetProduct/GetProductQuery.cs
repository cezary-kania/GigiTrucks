using GigiTrucks.Services.Products.Core.DTOs.Products;
using MediatR;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Products.GetProduct;

public record GetProductQuery(Guid ProductId) : IRequest<OneOf<ProductDto,NotFound>>;