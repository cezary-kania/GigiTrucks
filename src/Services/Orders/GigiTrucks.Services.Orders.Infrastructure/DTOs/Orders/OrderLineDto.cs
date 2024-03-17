namespace GigiTrucks.Services.Orders.Infrastructure.DTOs.Orders;

public record OrderLineDto(
    Guid Id,
    Guid ProductId,
    decimal UnitPrice,
    int Quantity);