using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.CreateNewsletter;

public class CreateNewsletterHandler : IRequestHandler<CreateNewsletter>
{
    public Task Handle(CreateNewsletter request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}