using GigiTrucks.Services.Newsletter.Domain.ValueTypes;

namespace GigiTrucks.Services.Newsletter.Domain.Entities;

public class Subscription
{
    public SubscriptionId Id { get; }
    public SubscriberId SubscriberId { get; }
    public Subscriber Subscriber { get; set; }
    public SubscriptionStatus IsActive { get; private set; } = false;

    protected Subscription()
    {
    }

    public Subscription(SubscriptionId id, SubscriberId subscriberId, SubscriptionStatus isActive)
    {
        Id = id;
        IsActive = isActive;
        SubscriberId = subscriberId;
    }    
    
    public void ChangeStatus(SubscriptionStatus isActive)
    {
        IsActive = isActive;
    }
}