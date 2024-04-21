namespace GigiTrucks.Services.Carts.Contracts.Dtos;

public record CartItemDto
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
}