using GigiTrucks.Services.Carts.Application.Exceptions;
using GigiTrucks.Services.Carts.Domain.Repositories;
using GigiTrucks.Services.Common.Messaging;
using GigiTrucks.Services.Orders.Contracts;
using MassTransit;

namespace GigiTrucks.Services.Carts.Application.EventHandlers.Orders;

public class OrderCreatedEventHandler(ICartRepository cartRepository) 
    : IConsumer<OrderCreatedEvent>, IIntegrationEventHandler
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var cart = await cartRepository.GetByIdAsync(context.Message.CartId);
        if (cart is null)
        {
            throw new CartNotFoundException();
        }
        await cartRepository.DeleteAsync(cart);
    }
}