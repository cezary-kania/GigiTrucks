using GigiTrucks.Services.Carts.Application.DTOs;
using GigiTrucks.Services.Carts.Application.Exceptions;
using GigiTrucks.Services.Carts.Domain.Repositories;
using Mapster;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Queries.GetCart;

public class GetCartHandler(ICartRepository cartRepository) : IRequestHandler<GetCart, CartDetailsDto>
{
    public async Task<CartDetailsDto> Handle(GetCart request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAsync(request.CustomerId);
        if (cart is null)
        {
            throw new CartNotFoundException();
        }

        return cart.Adapt<CartDetailsDto>();
    }
}