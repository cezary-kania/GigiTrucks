using MediatR;

namespace GigiTrucks.Services.Orders.Application.Commands.ApproveOrder;

public record ApproveOrder(Guid OrderId) : IRequest;