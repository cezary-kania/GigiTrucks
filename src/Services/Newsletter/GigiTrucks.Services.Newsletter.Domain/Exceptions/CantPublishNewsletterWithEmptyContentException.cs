namespace GigiTrucks.Services.Newsletter.Domain.Exceptions;

public class CantPublishNewsletterWithEmptyContentException() 
    : DomainException("Can't publish newsletter with empty content");