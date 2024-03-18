using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Carts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => 
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}