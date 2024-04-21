using System.Reflection;
using GigiTrucks.Services.Carts.Application.EventHandlers.Orders;
using GigiTrucks.Services.Orders.Contracts;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Carts.Application.EventHandlers;

public static class DependencyInjection
{
    public static IServiceCollection AddEventHandlers(this IServiceCollection services)
    {
        services.AddScoped<IConsumer<OrderCreatedEvent>, OrderCreatedEventHandler>();
        return services;
    }
}