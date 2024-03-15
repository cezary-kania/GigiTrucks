namespace GigiTrucks.Services.Orders.Domain.ValueObjects;

public record struct OrderId(Guid Value)
{
    public static OrderId Create() => new(Guid.NewGuid());    
    
    public static implicit operator Guid(OrderId orderId)
        => orderId.Value;
    
    public static implicit operator OrderId(Guid orderId)
        => new(orderId);
}