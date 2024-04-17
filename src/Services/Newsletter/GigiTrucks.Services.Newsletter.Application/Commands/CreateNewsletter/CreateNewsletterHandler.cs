using GigiTrucks.Services.Newsletter.Domain.Enums;
using GigiTrucks.Services.Newsletter.Domain.Repositories;
using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.CreateNewsletter;

public class CreateNewsletterHandler(INewsletterRepository newsletterRepository) 
    : IRequestHandler<CreateNewsletter>
{
    public async Task Handle(CreateNewsletter request, CancellationToken cancellationToken)
    {
        var newsletter = new Domain.Entities.Newsletter(request.NewsletterId, PublicationStatus.Draft);
        await newsletterRepository.AddAsync(newsletter);
    }
}