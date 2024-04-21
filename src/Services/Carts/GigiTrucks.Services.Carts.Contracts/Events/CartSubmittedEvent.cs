using GigiTrucks.Services.Carts.Contracts.Dtos;

namespace GigiTrucks.Services.Carts.Contracts.Events;

public record CartSubmittedEvent
{
    public Guid CartId { get; init; }
    public Guid CustomerId { get; init; }
    public IEnumerable<CartItemDto> Items { get; init; }
};