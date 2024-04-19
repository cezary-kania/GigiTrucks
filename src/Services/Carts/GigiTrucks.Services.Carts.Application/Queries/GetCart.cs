using GigiTrucks.Services.Carts.Application.DTOs;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Queries;

public record GetCart(Guid CustomerId) : IRequest<CartDetailsDto>;