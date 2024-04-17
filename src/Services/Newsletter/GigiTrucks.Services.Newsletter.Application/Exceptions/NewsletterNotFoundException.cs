namespace GigiTrucks.Services.Newsletter.Application.Exceptions;

public class NewsletterNotFoundException(Guid newsletterId) 
    : ApplicationException($"Newsletter with id: \"{newsletterId}\" was not found.");