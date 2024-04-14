namespace GigiTrucks.Services.Newsletter.Domain.ValueTypes;

public record struct SubscriptionId(Guid Value)
{
    public static SubscriptionId Create()
        => Guid.NewGuid();
    
    public static implicit operator Guid(SubscriptionId subscriptionId)
        => subscriptionId.Value;
    
    public static implicit operator SubscriptionId(Guid subscriptionId)
        => new(subscriptionId);
}