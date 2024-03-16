using GigiTrucks.Services.Orders.Domain.Repositories;
using GigiTrucks.Services.Orders.Infrastructure.DAL.EF;
using GigiTrucks.Services.Orders.Infrastructure.DAL.EF.Interceptors;
using GigiTrucks.Services.Orders.Infrastructure.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Orders.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<OrdersDbContext>(
            (sp, options) => options
                .UseSqlServer(configuration.GetConnectionString("OrdersDB"))
                .AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()));
        services.AddHostedService<DbInitializer>();
        return services;
    }
}