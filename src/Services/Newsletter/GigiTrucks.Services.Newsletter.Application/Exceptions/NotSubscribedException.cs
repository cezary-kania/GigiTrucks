namespace GigiTrucks.Services.Newsletter.Application.Exceptions;

public class NotSubscribedException(Guid subscriberId) 
    : ApplicationException($"{subscriberId} is not subscribing to newsletter.");