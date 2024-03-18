using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.CreateCart;

internal sealed class CreateCartHandler : IRequestHandler<CreateCart>
{
    public Task Handle(CreateCart request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}