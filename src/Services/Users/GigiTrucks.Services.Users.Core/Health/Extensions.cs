using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Users.Core.Health;

public static class Extensions
{
    public static IHealthChecksBuilder AddInfrastructureHealthChecks(
        this IHealthChecksBuilder builder,
        ConfigurationManager configuration)
    {
        builder.AddNpgSql(
            connectionString: configuration.GetConnectionString("Postgres")!,
            name: "PostgreSQL/Users DB");
        return builder;
    }
}