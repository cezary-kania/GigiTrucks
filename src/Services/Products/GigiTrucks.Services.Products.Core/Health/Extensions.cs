using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GigiTrucks.Services.Products.Health;

public static class Extensions
{
    public static IHealthChecksBuilder AddInfrastructureHealthChecks(
        this IHealthChecksBuilder builder,
        ConfigurationManager configuration)
    {
        builder.AddNpgSql(
            connectionString: configuration.GetConnectionString("Postgres")!,
            name: "PostgreSQL/Products DB");

        builder.AddAzureBlobStorage(
            connectionString: configuration["BlobStorageSettings:ConnectionString"]!,
            name: "Azure Blob Storage");
        return builder;
    }
}