namespace GigiTrucks.Services.Orders.Application.Exceptions;

public class OrderNotFoundException(Guid orderId) 
    : ResourceNotFoundException($"Order with Id: \"{orderId}\" does not exist");