using GigiTrucks.Services.Orders.Domain.Entities;
using GigiTrucks.Services.Orders.Domain.ValueObjects;

namespace GigiTrucks.Services.Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);
    Task<Order?> GetAsync(OrderId orderId);
}