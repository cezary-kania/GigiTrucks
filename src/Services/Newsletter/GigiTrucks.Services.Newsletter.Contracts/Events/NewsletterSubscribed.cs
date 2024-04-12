namespace GigiTrucks.Services.Newsletter.Contracts.Events;

public record NewsletterSubscribed
{
    public Guid SubscriberId { get; init; }   
}