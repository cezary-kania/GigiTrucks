namespace GigiTrucks.Services.Orders.Domain.ValueTypes;

public record struct UnitPrice(decimal Value)
{
    public static implicit operator decimal(UnitPrice unitPrice)
        => unitPrice.Value;
    
    public static implicit operator UnitPrice(decimal unitPrice)
        => new(unitPrice);
}