﻿using GigiTrucks.Services.Carts.Domain.Repositories;
using GigiTrucks.Services.Carts.Infrastructure.DAL.Redis;
using GigiTrucks.Services.Carts.Infrastructure.DAL.Redis.Repositories;
using GigiTrucks.Services.Common.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Carts.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddRedis(configuration);
        services.AddMessaging(configuration);
        services.AddScoped<ICartRepository,CartRepository>();
        return services;
    }
}