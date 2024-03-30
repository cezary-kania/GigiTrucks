using MediatR;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Products.CreateProduct;

public record CreateProductCommand(
    Guid CategoryId,
    string Name,
    string Description) : IRequest<OneOf<Success,Error<string>>>;