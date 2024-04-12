namespace GigiTrucks.Services.Newsletter.Application.Exceptions;

public class AlreadySubscribedException(Guid subscriberId) 
    : ApplicationException($"Newsletter already subscribed by{subscriberId}");