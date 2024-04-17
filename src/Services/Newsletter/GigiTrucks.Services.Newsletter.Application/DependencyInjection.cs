using System.Reflection;
using FluentValidation;
using GigiTrucks.Services.Common.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Newsletter.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCurrentUserService();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}