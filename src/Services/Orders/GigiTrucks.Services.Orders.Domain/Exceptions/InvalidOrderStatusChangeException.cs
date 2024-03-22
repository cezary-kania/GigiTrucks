using GigiTrucks.Services.Orders.Domain.Enums;
using GigiTrucks.Services.Orders.Domain.ValueObjects;

namespace GigiTrucks.Services.Orders.Domain.Exceptions;

public class InvalidOrderStatusChangeException (OrderId orderId, OrderStatus newStatus)
    : DomainException($"Can not change status of order: \"{orderId}\" to \"{newStatus}\"");