using MediatR;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid ProductId,
    Guid CategoryId,
    string Name,
    string Description) : IRequest<OneOf<Success,NotFound,Error<string>>>;