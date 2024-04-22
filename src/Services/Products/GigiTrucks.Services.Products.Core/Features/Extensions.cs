using System.Reflection;
using Carter;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Products.Core.Features;

internal static class Extensions
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddCarter();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return services;
    }
    
    public static IEndpointRouteBuilder MapFeatures(this IEndpointRouteBuilder builder)
    {
        builder.MapCarter();
        return builder;
    }
}