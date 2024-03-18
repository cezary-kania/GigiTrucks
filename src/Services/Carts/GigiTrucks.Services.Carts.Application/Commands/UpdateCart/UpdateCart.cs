using GigiTrucks.Services.Carts.Application.DTOs;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.UpdateCart;

public record UpdateCart(Guid CartId, IList<CartItemDto> Items) : IRequest;