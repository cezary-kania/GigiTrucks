using MediatR;

namespace GigiTrucks.Services.Orders.Application.Commands.CreateOrder;

public record CreateOrder(Guid cartId) : IRequest;