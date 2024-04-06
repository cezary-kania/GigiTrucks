using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.DTOs.Categories;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OneOf.Types;

namespace GigiTrucks.Services.Products.Core.Features.Categories.GetCategoryDetails;

internal sealed class GetCategoryQueryHandler(ProductsDbContext dbContext) 
    : IRequestHandler<GetCategoryQuery, OneOf<CategoryDto, NotFound>>
{
    public async Task<OneOf<CategoryDto, NotFound>> Handle(
        GetCategoryQuery request, 
        CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (category is null)
        {
            return new NotFound();
        }
        
        return category.Adapt<CategoryDto>();
    }
}