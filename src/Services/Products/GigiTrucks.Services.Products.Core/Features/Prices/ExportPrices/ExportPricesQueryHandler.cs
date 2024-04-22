using System.Globalization;
using CsvHelper;
using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.DTOs.Prices;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Products.Core.Features.Prices.ExportPrices;

internal sealed class ExportPricesQueryHandler(
    ProductsDbContext dbContext,
    TimeProvider timeProvider)
    : IRequestHandler<ExportPricesQuery, PriceListExportDto>
{
    public async Task<PriceListExportDto> Handle(ExportPricesQuery request, CancellationToken cancellationToken)
    {
        var pricesToExport = await GetPrices(request, cancellationToken);

        return new PriceListExportDto
        {
            Content = ConvertToCsv(pricesToExport),
            ContentType = "text/csv",
            ExportName = $"PriceExport-{timeProvider.GetUtcNow().DateTime.ToShortDateString()}"
        };
    }

    private static byte[] ConvertToCsv(IEnumerable<UploadedPriceDto> pricesToExport)
    {
        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(pricesToExport);
        writer.Flush();
        return memoryStream.ToArray();
    }

    private async Task<IEnumerable<UploadedPriceDto>> GetPrices(
        ExportPricesQuery request, 
        CancellationToken cancellationToken)
    {
        var query = dbContext.Prices.AsQueryable();
        if (request.PricesIds is not null)
        {
            query = query.Join(
                request.PricesIds,
                up => up.Id,
                id => id,
                (up, id) => up);
        }
        
        if (request.ProductIds is not null)
        {
            query = query.Join(
                request.ProductIds,
                up => up.ProductId,
                id => id,
                (up, id) => up);
        }
        
        var prices = await query.ToListAsync(cancellationToken);
        return prices.Adapt<IEnumerable<UploadedPriceDto>>();
    }
}