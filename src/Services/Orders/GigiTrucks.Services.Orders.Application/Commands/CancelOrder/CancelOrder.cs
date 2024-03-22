using MediatR;

namespace GigiTrucks.Services.Orders.Application.Commands.CancelOrder;

public record CancelOrder(Guid OrderId) : IRequest;