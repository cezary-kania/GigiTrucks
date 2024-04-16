using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.UpdateNewsletter;

public class UpdateNewsletterHandler : IRequestHandler<UpdateNewsletter>
{
    public Task Handle(UpdateNewsletter request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}