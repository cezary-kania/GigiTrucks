using GigiTrucks.Services.Common.Exceptions;

namespace GigiTrucks.Services.Carts.Application.Exceptions;

public class CartNotFoundException() 
    : ResourceNotFoundException("Cart was not found.");