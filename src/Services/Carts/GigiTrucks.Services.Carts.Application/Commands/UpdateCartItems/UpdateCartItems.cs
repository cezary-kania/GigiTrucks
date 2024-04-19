using GigiTrucks.Services.Carts.Application.DTOs;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.UpdateCartItems;

public record UpdateCartItems(Guid CustomerId, IList<CartItemDto> Items) : IRequest;