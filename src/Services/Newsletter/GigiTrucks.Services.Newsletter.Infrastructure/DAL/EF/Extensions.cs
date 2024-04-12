using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Newsletter.Infrastructure.DAL.EF;

public static class Extensions
{
    public static IServiceCollection AddNewsletterDb(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        services.AddDbContext<NewsletterDbContext>(options => options.UseNpgsql(connectionString));
        services.AddHostedService<DbContextInitializer>();
        return services;
    }
}