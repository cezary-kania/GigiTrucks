using GigiTrucks.Services.Carts.Application.Exceptions;
using GigiTrucks.Services.Carts.Domain.Repositories;
using GigiTrucks.Services.Carts.Domain.ValueTypes;
using Mapster;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.UpdateCart;

public class UpdateCartHandler(ICartRepository cartRepository) : IRequestHandler<UpdateCart>
{
    public async Task Handle(UpdateCart request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAsync(request.CustomerId);
        if (cart is null)
        {
            throw new CartNotCreatedException();
        }

        cart.SetItems(request.Items.Adapt<List<CartItem>>());

        await cartRepository.UpdateAsync(cart);
    }
}