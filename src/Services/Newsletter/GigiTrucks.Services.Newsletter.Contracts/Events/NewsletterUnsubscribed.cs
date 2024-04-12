namespace GigiTrucks.Services.Newsletter.Contracts.Events;

public record NewsletterUnsubscribed
{
    public Guid SubscriberId { get; init; }   
}