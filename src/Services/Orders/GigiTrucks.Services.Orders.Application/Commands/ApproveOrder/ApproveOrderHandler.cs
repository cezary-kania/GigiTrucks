using GigiTrucks.Services.Orders.Application.Exceptions;
using GigiTrucks.Services.Orders.Domain.Repositories;
using MediatR;

namespace GigiTrucks.Services.Orders.Application.Commands.ApproveOrder;

public class ApproveOrderHandler(IOrderRepository orderRepository) : IRequestHandler<ApproveOrder>
{
    public async Task Handle(ApproveOrder request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetAsync(request.OrderId)
                    ?? throw new OrderNotFoundException(request.OrderId);
        
        order.Approve();
        await orderRepository.UpdateAsync(order);
    }
}