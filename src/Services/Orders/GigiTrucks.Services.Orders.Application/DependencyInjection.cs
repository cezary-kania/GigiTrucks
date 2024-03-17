using System.Reflection;
using GigiTrucks.Services.Orders.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Orders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}