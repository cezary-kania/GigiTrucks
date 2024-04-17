using FluentValidation;
using GigiTrucks.Services.Newsletter.Application.Exceptions;
using GigiTrucks.Services.Newsletter.Domain.Repositories;
using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.UpdateNewsletter;

public class UpdateNewsletterHandler(
    INewsletterRepository newsletterRepository, 
    IValidator<UpdateNewsletter> validator
    ) : IRequestHandler<UpdateNewsletter>
{
    public async Task Handle(UpdateNewsletter request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var newsletter = await newsletterRepository.GetAsync(request.NewsletterId);
        if (newsletter is null)
        {
            throw new NewsletterNotFoundException(request.NewsletterId);
        }
        
        newsletter.UpdateBody(request.Title, request.Content);
        await newsletterRepository.UpdateAsync(newsletter);
    }
}