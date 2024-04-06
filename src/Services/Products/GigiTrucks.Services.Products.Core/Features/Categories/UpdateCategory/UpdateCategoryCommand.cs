using MediatR;
using OneOf.Types;
using OneOf;

namespace GigiTrucks.Services.Products.Core.Features.Categories.UpdateCategory;

public record UpdateCategoryCommand(
    Guid CategoryId,
    string Name, 
    Guid? ParentCategoryId) : IRequest<OneOf<Success,NotFound,Error<string>>>;