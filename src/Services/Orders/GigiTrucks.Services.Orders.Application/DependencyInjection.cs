using System.Reflection;
using GigiTrucks.Services.Common.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Orders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCurrentUserService();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}