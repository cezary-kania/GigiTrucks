using MediatR;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Products.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId) : IRequest<OneOf<Success,NotFound>>;
