namespace GigiTrucks.Services.Carts.Domain.ValueTypes;

public record struct ProductId(Guid Value)
{
    public static implicit operator Guid(ProductId productId)
        => productId.Value;
    
    public static implicit operator ProductId(Guid productId)
        => new(productId);
}