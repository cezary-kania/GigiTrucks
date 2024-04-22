using GigiTrucks.Services.Products.Core.DTOs.Prices;
using MediatR;

namespace GigiTrucks.Services.Products.Core.Features.Prices.ExportPrices;

public record ExportPricesQuery : IRequest<PriceListExportDto>
{
    public List<Guid>? PricesIds { get; init; }
    public List<Guid>? ProductIds { get; init; }
}