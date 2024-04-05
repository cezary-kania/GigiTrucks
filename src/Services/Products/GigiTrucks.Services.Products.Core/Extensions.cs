using Azure.Storage;
using GigiTrucks.Services.Products.Core.DAL.BlobStorage;
using GigiTrucks.Services.Products.Core.DAL.EF;
using GigiTrucks.Services.Products.Core.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GigiTrucks.Services.Products.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<BlobStorageSettings>(configuration.GetSection(nameof(BlobStorageSettings)));
        
        services.AddAzureClients(clientBuilder =>
        {
            var blobSettings = services.BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<BlobStorageSettings>>().CurrentValue;
            clientBuilder.AddBlobServiceClient(blobSettings.ConnectionString);
        });
        services.AddFeatures();
        services.AddProductsDb(configuration);
        return services;
    }
    
    public static WebApplication UseCore(this WebApplication services)
    {
        services.MapFeatures();
        return services;
    }
}