using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.DeleteCart;

internal sealed class DeleteCartHandler : IRequestHandler<DeleteCart>
{
    public Task Handle(DeleteCart request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}