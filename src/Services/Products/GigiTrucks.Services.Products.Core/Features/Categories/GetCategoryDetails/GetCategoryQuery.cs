using GigiTrucks.Services.Products.Core.DTOs.Categories;
using MediatR;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Categories.GetCategoryDetails;

public record GetCategoryQuery(Guid CategoryId) : IRequest<OneOf<CategoryDto, NotFound>>;