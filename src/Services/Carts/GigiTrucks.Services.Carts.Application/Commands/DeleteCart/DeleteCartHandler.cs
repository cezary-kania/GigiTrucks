using GigiTrucks.Services.Carts.Application.Exceptions;
using GigiTrucks.Services.Carts.Domain.Repositories;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.DeleteCart;

internal sealed class DeleteCartHandler(ICartRepository cartRepository) : IRequestHandler<DeleteCart>
{
    public async Task Handle(DeleteCart request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAsync(request.CustomerId);
        if (cart is null)
        {
            throw new CartNotFoundException();
        }
        await cartRepository.DeleteAsync(cart);
    }
}