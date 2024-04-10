using GigiTrucks.Services.Orders.Domain.ValueTypes;

namespace GigiTrucks.Services.Orders.Domain.Exceptions;

public class OrderLineAlreadyAddedException(OrderLineId orderLineId)
    : DomainException($"Order line: \"{orderLineId}\" already added to order");