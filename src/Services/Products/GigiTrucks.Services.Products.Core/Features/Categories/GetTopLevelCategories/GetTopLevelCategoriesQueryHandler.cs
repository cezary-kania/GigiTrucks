using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.DTOs.Categories;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Products.Core.Features.Categories.GetTopLevelCategories;

internal sealed class GetTopLevelCategoriesQueryHandler(ProductsDbContext dbContext) 
    : IRequestHandler<GetTopLevelCategoriesQuery,IEnumerable<CategoryWithoutProductsDto>>
{
    public async Task<IEnumerable<CategoryWithoutProductsDto>> Handle(
        GetTopLevelCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        var categories = await dbContext.Categories
            .Where(x => x.ParentCategory == null)
            .Include(x => x.SubCategories)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return categories.Adapt<IEnumerable<CategoryWithoutProductsDto>>();
    }
}