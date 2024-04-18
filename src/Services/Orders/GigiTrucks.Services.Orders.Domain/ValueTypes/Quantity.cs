namespace GigiTrucks.Services.Orders.Domain.ValueTypes;

public record struct Quantity(int Value)
{
    public static implicit operator int(Quantity quantity)
        => quantity.Value;
    
    public static implicit operator Quantity(int quantity)
        => new(quantity);
}