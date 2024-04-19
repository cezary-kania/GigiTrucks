using GigiTrucks.Services.Common.Exceptions;

namespace GigiTrucks.Services.Carts.Application.Exceptions;

public class CartNotCreatedException() 
    : ResourceNotFoundException("Cart not created.");