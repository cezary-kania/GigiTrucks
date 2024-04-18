namespace GigiTrucks.Services.Carts.Domain.ValueTypes;

public record CartItem
{
    public ProductId ProductId { get; init; }
    public Quantity Quantity { get; init; }
    public OrderNo DisplayOrderNo { get; init; }
}