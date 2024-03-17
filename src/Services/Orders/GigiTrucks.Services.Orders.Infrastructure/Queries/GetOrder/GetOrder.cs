using GigiTrucks.Services.Orders.Infrastructure.DTOs.Orders;
using MediatR;

namespace GigiTrucks.Services.Orders.Infrastructure.Queries.GetOrder;

public record GetOrder(Guid OrderId) : IRequest<OrderDetailsDto>;
