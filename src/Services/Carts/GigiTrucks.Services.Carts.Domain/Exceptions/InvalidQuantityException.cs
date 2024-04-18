using GigiTrucks.Services.Carts.Domain.ValueTypes;

namespace GigiTrucks.Services.Carts.Domain.Exceptions;

public class InvalidQuantityException() 
    : DomainException($"Quantity must be greater than {Quantity.MinValue}");