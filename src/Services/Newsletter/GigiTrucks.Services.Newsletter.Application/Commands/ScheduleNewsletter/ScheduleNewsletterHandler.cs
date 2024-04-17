using FluentValidation;
using GigiTrucks.Services.Newsletter.Application.Exceptions;
using GigiTrucks.Services.Newsletter.Domain.Repositories;
using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.ScheduleNewsletter;

public class ScheduleNewsletterHandler(
    INewsletterRepository newsletterRepository, 
    IValidator<ScheduleNewsletter> validator) 
    : IRequestHandler<ScheduleNewsletter>
{
    public async Task Handle(ScheduleNewsletter request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);
        
        var newsletter = await newsletterRepository.GetAsync(request.NewsletterId);
        if (newsletter is null)
        {
            throw new NewsletterNotFoundException(request.NewsletterId);
        }
        
        newsletter.Schedule(request.PublishAt);
        await newsletterRepository.UpdateAsync(newsletter);
    }
}