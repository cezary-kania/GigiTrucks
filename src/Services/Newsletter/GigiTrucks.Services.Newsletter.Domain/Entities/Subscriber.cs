using GigiTrucks.Services.Newsletter.Domain.ValueTypes;

namespace GigiTrucks.Services.Newsletter.Domain.Entities;

public class Subscriber
{
    public SubscriberId Id { get; }
    public Email Email { get; private set; }
    public SubscriptionStatus IsActive { get; private set; } = false;
    
    protected Subscriber()
    {
    }

    public Subscriber(SubscriberId id, Email email, SubscriptionStatus isActive)
    {
        Id = id;
        Email = email;
        IsActive = isActive;
    }
    
    public void UpdateSubscriptionStatus(SubscriptionStatus newStatus)
    {
        IsActive = newStatus;
    }
}