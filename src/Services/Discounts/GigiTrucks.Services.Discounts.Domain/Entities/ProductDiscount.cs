namespace GigiTrucks.Services.Discounts.Domain.Entities;

public class ProductDiscount : Discount
{
    public Guid ProductId { get; set; }
    public DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset ValidTo { get; set; }
}