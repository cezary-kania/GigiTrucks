using GigiTrucks.Services.Newsletter.Domain.Exceptions;
using GigiTrucks.Services.Newsletter.Domain.ValueTypes;

namespace GigiTrucks.Services.Newsletter.Domain.Entities;

public class Subscriber
{
    public SubscriberId Id { get; }
    public Subscription? Subscription { get; set; }
    public Email Email { get; private set; }
    
    protected Subscriber()
    {
    }

    public Subscriber(SubscriberId id, Email email)
    {
        Id = id;
        Email = email;
    }
    
        
    public void Subscribe()
    {
        if (HasActiveSubscription)
        {
            throw new AlreadySubscribedException();
        }
        
        if (Subscription is null)
        {
            Subscription = new Subscription(SubscriptionId.Create(), Id, true);
            return;
        }
        
        Subscription.ChangeStatus(true);
    }

    public void Unsubscribe()
    {
        if (!HasActiveSubscription)
        {
            throw new NotSubscribedException();
        }
        
        Subscription?.ChangeStatus(false);
    }
    
    public SubscriptionStatus HasActiveSubscription => Subscription?.IsActive ?? false;
}