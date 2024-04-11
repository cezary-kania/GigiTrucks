namespace GigiTrucks.Services.Newsletter.Domain.ValueTypes;

public record struct SubscriberId(Guid Value)
{
    public static implicit operator Guid(SubscriberId subscriberId)
        => subscriberId.Value;
    
    public static implicit operator SubscriberId(Guid subscriberId)
        => new(subscriberId);
}