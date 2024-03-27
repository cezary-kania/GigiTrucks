namespace GigiTrucks.Services.Products.Core.DTOs.Products;

public record ProductImageDto(
    Guid Id,
    string Uri,
    string Name,
    int DisplayOrder,
    Guid ProductId);