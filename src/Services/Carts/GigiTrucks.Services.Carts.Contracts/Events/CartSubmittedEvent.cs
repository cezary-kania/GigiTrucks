namespace GigiTrucks.Services.Carts.Contracts.Events;

public record CartSubmittedEvent
{
    public Guid CartId { get; init; }
};