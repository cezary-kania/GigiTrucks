using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Orders.Infrastructure.Health;

public static class Extensions
{
    public static IHealthChecksBuilder AddInfrastructureHealthChecks(
        this IHealthChecksBuilder builder,
        ConfigurationManager configuration)
    {
        builder.AddSqlServer(
            connectionString: configuration.GetConnectionString("OrdersDB")!,
            name: "SQL Server/Orders DB");
        return builder;
    }
}