namespace GigiTrucks.Services.Newsletter.Application.Exceptions;

public class SubscriberNotFoundException(Guid subscriberId) 
    : ApplicationException($"{subscriberId} was not found.");