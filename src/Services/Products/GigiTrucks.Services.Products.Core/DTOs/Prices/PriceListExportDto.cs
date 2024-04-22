namespace GigiTrucks.Services.Products.Core.DTOs.Prices;

public record PriceListExportDto
{
    public required byte[] Content { get; init; }
    public required string ContentType { get; init; }
    public required string ExportName { get; init; }
}