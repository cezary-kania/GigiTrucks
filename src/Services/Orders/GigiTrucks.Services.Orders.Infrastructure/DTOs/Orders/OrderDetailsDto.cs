namespace GigiTrucks.Services.Orders.Infrastructure.DTOs.Orders;

public record OrderDetailsDto(
    Guid Id,
    string Status,
    IList<OrderLineDto> OrderLines);