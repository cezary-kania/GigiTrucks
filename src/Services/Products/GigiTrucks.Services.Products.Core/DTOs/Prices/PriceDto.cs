namespace GigiTrucks.Services.Products.Core.DTOs.Prices;

public record PriceDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public bool IsActive { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = null!;
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}