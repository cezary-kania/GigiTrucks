namespace GigiTrucks.Services.Carts.Domain.ValueTypes;

public record struct CartId(Guid Value)
{
    public static implicit operator Guid(CartId cartId)
        => cartId.Value;
    
    public static implicit operator CartId(Guid cartId)
        => new(cartId);
}