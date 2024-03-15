namespace GigiTrucks.Services.Orders.Domain.ValueObjects;

public record struct ProductId(Guid Value)
{
    public static ProductId Create() => new(Guid.NewGuid());    
    
    public static implicit operator Guid(ProductId productId)
        => productId.Value;
    
    public static implicit operator ProductId(Guid productId)
        => new(productId);
}