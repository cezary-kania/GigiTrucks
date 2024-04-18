using GigiTrucks.Services.Carts.Domain.Exceptions;

namespace GigiTrucks.Services.Carts.Domain.ValueTypes;

public record struct Quantity
{
    public const int MinValue = 1;
    public int Value { get; }
    
    public static Quantity operator +(Quantity first, Quantity second)
        => new(first.Value + second.Value);
    
    public Quantity(int value)
    {
        if (value < MinValue)
        {
            throw new InvalidQuantityException();
        }
        
        Value = value;
    }
    
    public static implicit operator int(Quantity quantity)
        => quantity.Value;

    public static implicit operator Quantity(int value)
        => new(value);
}