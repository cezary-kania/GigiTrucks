using GigiTrucks.Services.Orders.Domain.Entities;
using GigiTrucks.Services.Orders.Domain.ValueTypes;

namespace GigiTrucks.Services.Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task<Order?> GetAsync(OrderId orderId);
}