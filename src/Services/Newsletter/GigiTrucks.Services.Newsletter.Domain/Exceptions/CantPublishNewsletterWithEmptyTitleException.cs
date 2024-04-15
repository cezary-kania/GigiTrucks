namespace GigiTrucks.Services.Newsletter.Domain.Exceptions;

public class CantPublishNewsletterWithEmptyTitleException()
    : DomainException("Can't publish newsletter with empty title");