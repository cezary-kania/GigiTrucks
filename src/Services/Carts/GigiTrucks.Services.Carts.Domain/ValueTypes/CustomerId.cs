namespace GigiTrucks.Services.Carts.Domain.ValueTypes;

public record struct CustomerId(Guid Value)
{
    public static implicit operator Guid(CustomerId customerId)
        => customerId.Value;
    
    public static implicit operator CustomerId(Guid customerId)
        => new(customerId);
}