using GigiTrucks.Services.Carts.Domain.Entities;
using GigiTrucks.Services.Carts.Domain.Repositories;
using GigiTrucks.Services.Carts.Domain.ValueTypes;
using Mapster;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.CreateCart;

internal sealed class CreateCartHandler(ICartRepository cartRepository) : IRequestHandler<CreateCart>
{
    public async Task Handle(CreateCart request, CancellationToken cancellationToken)
    {
        var cart = new Cart(request.CartId, request.CustomerId, request.Items.Adapt<List<CartItem>>());
        await cartRepository.AddAsync(cart);
    }
}