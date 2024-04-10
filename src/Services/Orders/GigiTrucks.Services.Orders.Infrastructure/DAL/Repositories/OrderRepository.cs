using GigiTrucks.Services.Orders.Domain.Entities;
using GigiTrucks.Services.Orders.Domain.Repositories;
using GigiTrucks.Services.Orders.Domain.ValueTypes;
using GigiTrucks.Services.Orders.Infrastructure.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Orders.Infrastructure.DAL.Repositories;

internal sealed class OrderRepository(OrdersDbContext dbContext) : IOrderRepository
{
    public async Task AddAsync(Order order)
    {
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Order order)
    {
        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Order?> GetAsync(OrderId orderId)
        => await dbContext.Orders
            .Include(o => o.OrderLines)
            .FirstOrDefaultAsync(order => order.Id == orderId);
}