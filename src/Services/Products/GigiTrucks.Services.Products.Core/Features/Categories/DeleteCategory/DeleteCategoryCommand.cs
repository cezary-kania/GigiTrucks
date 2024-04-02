using MediatR;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Categories.DeleteCategory;

public record DeleteCategoryCommand(Guid CategoryId) : IRequest<OneOf<Success,NotFound,Error<string>>>;