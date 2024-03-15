namespace GigiTrucks.Services.Orders.Domain.ValueObjects;

public record struct OrderLineId(Guid Value)
{
    public static OrderLineId Create() => new(Guid.NewGuid());    
    
    public static implicit operator Guid(OrderLineId orderLineId)
        => orderLineId.Value;
    
    public static implicit operator OrderLineId(Guid orderLineId)
        => new(orderLineId);
}