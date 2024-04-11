namespace GigiTrucks.Services.Newsletter.Domain.ValueTypes;

public record struct SubscriptionStatus(bool Value)
{
public static implicit operator bool(SubscriptionStatus subscriptionStatus)
    => subscriptionStatus.Value;
    
public static implicit operator SubscriptionStatus(bool subscriptionStatus)
    => new(subscriptionStatus);
}