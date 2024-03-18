using MediatR;

namespace GigiTrucks.Services.Orders.Application.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrder>
{
    public Task Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}