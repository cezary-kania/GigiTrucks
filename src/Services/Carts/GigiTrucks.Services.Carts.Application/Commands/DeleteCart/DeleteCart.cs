using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.DeleteCart;

public record DeleteCart(Guid CustomerId) : IRequest;