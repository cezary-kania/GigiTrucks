using GigiTrucks.Services.Newsletter.Infrastructure.DAL.EF;
using GigiTrucks.Services.Newsletter.Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Newsletter.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddMessaging(configuration);
        services.AddNewsletterDb(configuration);
        return services;
    }
}