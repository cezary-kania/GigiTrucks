using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Common.Identity;

public static class Extensions
{
    public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}