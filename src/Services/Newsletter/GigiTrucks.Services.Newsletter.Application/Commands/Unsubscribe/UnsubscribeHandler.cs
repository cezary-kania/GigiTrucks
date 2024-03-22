using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.Unsubscribe;

public class UnsubscribeHandler : IRequestHandler<Unsubscribe>
{
    public Task Handle(Unsubscribe request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}