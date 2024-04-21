namespace GigiTrucks.Services.Orders.Contracts;

public record OrderCreatedEvent
{
    public Guid CartId { get; init; }
};