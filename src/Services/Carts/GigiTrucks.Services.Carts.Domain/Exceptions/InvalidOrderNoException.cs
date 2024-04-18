using GigiTrucks.Services.Carts.Domain.ValueTypes;

namespace GigiTrucks.Services.Carts.Domain.Exceptions;

public class InvalidOrderNoException()
    : DomainException("OrderNo must be positive number");