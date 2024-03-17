namespace GigiTrucks.Services.Carts.Application.DTOs;

public record CartDetailsDto(
    Guid CartId,
    Guid CustomerId,
    IEnumerable<CartItemDto> Items);