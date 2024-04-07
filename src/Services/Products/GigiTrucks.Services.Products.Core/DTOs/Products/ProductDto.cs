namespace GigiTrucks.Services.Products.Core.DTOs.Products;

public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    IEnumerable<ProductImageDto> Images);