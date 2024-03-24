using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Products.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddProductsDb(configuration);
        return services;
    }
}