using GigiTrucks.Services.Products.Core.DTOs.Categories;
using MediatR;

namespace GigiTrucks.Services.Products.Core.Features.Categories.GetTopLevelCategories;

public record GetTopLevelCategoriesQuery : IRequest<IEnumerable<CategoryWithoutProductsDto>>;