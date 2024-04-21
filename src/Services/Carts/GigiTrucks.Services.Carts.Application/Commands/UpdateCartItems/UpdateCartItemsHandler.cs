using GigiTrucks.Services.Carts.Application.Exceptions;
using GigiTrucks.Services.Carts.Domain.Repositories;
using GigiTrucks.Services.Carts.Domain.ValueTypes;
using Mapster;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.UpdateCartItems;

public class UpdateCartItemsHandler(ICartRepository cartRepository) : IRequestHandler<UpdateCartItems>
{
    public async Task Handle(UpdateCartItems request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAsync(request.CustomerId);
        if (cart is null)
        {
            throw new CartNotFoundException();
        }

        cart.SetItems(request.Items.Adapt<List<CartItem>>());

        await cartRepository.UpdateAsync(cart);
    }
}