using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.UpdateCart;

public class UpdateCartHandler : IRequestHandler<UpdateCart>
{
    public Task Handle(UpdateCart request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}