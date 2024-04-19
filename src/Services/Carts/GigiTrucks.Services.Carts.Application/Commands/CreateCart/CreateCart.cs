using GigiTrucks.Services.Carts.Application.DTOs;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.CreateCart;

public record CreateCart(Guid CustomerId, IList<CartItemDto> Items) : IRequest;
