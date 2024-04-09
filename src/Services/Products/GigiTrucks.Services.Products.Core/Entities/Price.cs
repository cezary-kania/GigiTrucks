using GigiTrucks.Services.Products.Core.Enums;

namespace GigiTrucks.Services.Products.Core.Entities;

public class Price
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public bool IsActive { get; set; }
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}