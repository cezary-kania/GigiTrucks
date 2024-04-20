using GigiTrucks.Services.Products.Core.DTOs.Common;
using GigiTrucks.Services.Products.Core.DTOs.Prices;
using MediatR;

namespace GigiTrucks.Services.Products.Core.Features.Prices.GetPrices;

public record GetPricesQuery : IRequest<PagedList<PriceDto>>
{
    private const int MaxPageSize = 50;
    private const int FirstPageNo = 1;
    public string? SortColumn { get; init; }
    public string? SortOrder { get; init; }
    public int? PageSize { get; init; }
    public int? PageNumber { get; init; }
    public int PageNo => PageNumber ?? FirstPageNo;
    public int CalculatedPageSize => PageSize ?? MaxPageSize;
}
    