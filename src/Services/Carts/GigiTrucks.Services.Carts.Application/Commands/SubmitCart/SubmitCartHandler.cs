using GigiTrucks.Services.Carts.Application.Exceptions;
using GigiTrucks.Services.Carts.Domain.Enums;
using GigiTrucks.Services.Carts.Domain.Repositories;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.SubmitCart;

public class SubmitCartHandler(ICartRepository cartRepository) : IRequestHandler<SubmitCart>
{
    public async Task Handle(SubmitCart request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAsync(request.CustomerId);
        if (cart is null)
        {
            throw new CartNotCreatedException();
        }

        cart.Submit();

        await cartRepository.UpdateAsync(cart);
    }
}