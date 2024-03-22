using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.Subscribe;

public class SubscribeHandler : IRequestHandler<Subscribe>
{
    public Task Handle(Subscribe request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}