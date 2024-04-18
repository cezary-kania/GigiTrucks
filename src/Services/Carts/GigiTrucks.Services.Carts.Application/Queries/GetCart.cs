using GigiTrucks.Services.Carts.Application.DTOs;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Queries;

public class GetCart(Guid cartId) : IRequest<CartDetailsDto>;