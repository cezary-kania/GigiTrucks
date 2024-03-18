using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.SubmitCart;

public class SubmitCartHandler : IRequestHandler<SubmitCart>
{
    public Task Handle(SubmitCart request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}