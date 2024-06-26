﻿using GigiTrucks.Services.Carts.Application.Exceptions;
using GigiTrucks.Services.Carts.Contracts.Events;
using GigiTrucks.Services.Carts.Domain.Repositories;
using Mapster;
using MassTransit;
using MediatR;

namespace GigiTrucks.Services.Carts.Application.Commands.SubmitCart;

public class SubmitCartHandler(
    ICartRepository cartRepository, 
    IPublishEndpoint publishEndpoint) : IRequestHandler<SubmitCart>
{
    public async Task Handle(SubmitCart request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetAsync(request.CustomerId);
        if (cart is null)
        {
            throw new CartNotFoundException();
        }

        cart.Submit();
        await cartRepository.PersistAsync(cart);

        var cartSubmittedEvent = cart.Adapt<CartSubmittedEvent>();
        await publishEndpoint.Publish(cartSubmittedEvent, cancellationToken);
    }
}