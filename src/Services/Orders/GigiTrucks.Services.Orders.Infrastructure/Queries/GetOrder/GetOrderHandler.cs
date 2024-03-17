using GigiTrucks.Services.Orders.Application.Exceptions;
using GigiTrucks.Services.Orders.Infrastructure.DAL.EF;
using GigiTrucks.Services.Orders.Infrastructure.DTOs.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Mapster;

namespace GigiTrucks.Services.Orders.Infrastructure.Queries.GetOrder;

public class GetOrderHandler(
    OrdersDbContext ordersDbContext) : IRequestHandler<GetOrder,OrderDetailsDto>
{
    public async Task<OrderDetailsDto> Handle(GetOrder request, CancellationToken cancellationToken)
    {
        var order = await ordersDbContext.Orders
            .Include(o => o.OrderLines)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(request.OrderId);
        }

        return order.Adapt<OrderDetailsDto>();
    }
}