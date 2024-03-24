using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Products.Core.DAL.EF;

internal static class Extensions
{
    public static IServiceCollection AddProductsDb(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        services.AddDbContext<ProductsDbContext>(options => options.UseNpgsql(connectionString));
        services.AddHostedService<DbContextInitializer>();
        return services;
    }
}