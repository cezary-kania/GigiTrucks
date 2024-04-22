namespace GigiTrucks.Services.Products.Core.Features.Prices.UploadPriceFile;

public record UploadedPrice
{
    public Guid Id { get; init; }
    public Guid ProductId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = null!;
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}