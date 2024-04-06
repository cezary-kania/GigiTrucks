using GigiTrucks.Services.Products.Core.DTOs.Products;

namespace GigiTrucks.Services.Products.Core.DTOs.Categories;

public record CategoryDto(
    Guid Id,
    string Name,
    IEnumerable<ProductPreviewDto> Products,
    IEnumerable<CategoryWithoutProductsDto> SubCategories);