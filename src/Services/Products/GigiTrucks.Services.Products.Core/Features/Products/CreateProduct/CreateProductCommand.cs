using MediatR;

namespace GigiTrucks.Services.Products.Core.Features.Products.CreateProduct;

public record CreateProductCommand(
    Guid ProductId,
    string Name,
    string Description) : IRequest;