using System.Linq.Expressions;
using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.DTOs.Common;
using GigiTrucks.Services.Products.Core.DTOs.Prices;
using GigiTrucks.Services.Products.Core.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Products.Core.Features.Prices.GetPrices;

internal sealed class GetPricesQueryHandler(ProductsDbContext dbContext) 
    : IRequestHandler<GetPricesQuery,PagedList<PriceDto>>
{
    public async Task<PagedList<PriceDto>> Handle(GetPricesQuery request, CancellationToken cancellationToken)
    {
        var pricesQuery =  dbContext.Prices
            .Include(x => x.Product)
            .AsQueryable();

        pricesQuery = request.SortOrder?.ToLower() == "desc" 
            ? pricesQuery.OrderByDescending(GetSortProperty(request)) 
            : pricesQuery.OrderBy(GetSortProperty(request));
        
        var totalCount = await pricesQuery.CountAsync(cancellationToken);
        
        var prices = await pricesQuery
            .Skip((request.PageNo - 1) * request.CalculatedPageSize)
            .Take(request.CalculatedPageSize)
            .ToListAsync(cancellationToken);
        
        return new PagedList<PriceDto>(
            prices.Adapt<List<PriceDto>>(), 
            request.PageNo, 
            request.CalculatedPageSize, 
            totalCount);
    }

    private static Expression<Func<Price, object>> GetSortProperty(GetPricesQuery request) 
        => request.SortColumn switch
        {
            "productName" => price => price.Product.Name,
            "productId" => price => price.ProductId,
            "isActive" => price => price.IsActive,
            "currency" => price => price.Currency,
            "validFrom" => price => price.ValidFrom,
            "validTo" => price => price.ValidTo,
            _ => price => price.Id,
        };
}