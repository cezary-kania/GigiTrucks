using GigiTrucks.Services.Orders.Application.Exceptions;
using GigiTrucks.Services.Orders.Domain.Repositories;
using MediatR;

namespace GigiTrucks.Services.Orders.Application.Commands.CancelOrder;

public class CancelOrderHandler(IOrderRepository orderRepository) : IRequestHandler<CancelOrder>
{
    public async Task Handle(CancelOrder request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetAsync(request.OrderId)
                    ?? throw new OrderNotFoundException(request.OrderId);

        order.Cancel();
        await orderRepository.UpdateAsync(order);
    }
}