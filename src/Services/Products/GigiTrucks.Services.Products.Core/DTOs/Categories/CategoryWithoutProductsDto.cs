namespace GigiTrucks.Services.Products.Core.DTOs.Categories;

public record CategoryWithoutProductsDto(
    Guid Id,
    string Name,
    IEnumerable<CategoryWithoutProductsDto> SubCategories);