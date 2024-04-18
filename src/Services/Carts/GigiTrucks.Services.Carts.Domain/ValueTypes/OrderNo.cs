using GigiTrucks.Services.Carts.Domain.Exceptions;

namespace GigiTrucks.Services.Carts.Domain.ValueTypes;

public record struct OrderNo
{
    private const int MinValue = 1;
    public int Value { get; }

    public OrderNo(int value)
    {
        if (value < MinValue)
        {
            throw new InvalidOrderNoException();
        }
        
        Value = value;
    }
    
    public static implicit operator int(OrderNo orderNo)
        => orderNo.Value;

    public static implicit operator OrderNo(int orderNo)
        => new(orderNo);
}