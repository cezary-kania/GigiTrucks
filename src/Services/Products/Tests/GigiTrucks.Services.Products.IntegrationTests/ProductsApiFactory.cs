using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using GigiTrucks.Services.Products.Core.DAL.BlobStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Time.Testing;
using Testcontainers.Azurite;
using Testcontainers.PostgreSql;
using Xunit;

namespace GigiTrucks.Services.Products.IntegrationTests;

public class ProductsApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();
    
    private readonly AzuriteContainer _azuriteContainer = new AzuriteBuilder()
        .Build();
    
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var x = _azuriteContainer.GetConnectionString();
        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:Postgres", _dbContainer.GetConnectionString() },
                { "BlobStorageSettings:ConnectionString", _azuriteContainer.GetConnectionString() },
            }!);
        });
        return base.CreateHost(builder);
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureTestServices(services =>
        {
            var fakeTimeProvider = new FakeTimeProvider();
            fakeTimeProvider.SetUtcNow(new DateTime(2024, 1,1));
            services.AddSingleton(fakeTimeProvider as TimeProvider);
            
            var blobServiceClient = services.BuildServiceProvider().GetRequiredService<BlobServiceClient>();
            var blobSettings = services.BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<BlobStorageSettings>>().CurrentValue;
            
            var container = blobServiceClient.CreateBlobContainer(
                blobSettings.ProductImagesContainerName);
            container.Value.SetAccessPolicy(PublicAccessType.Blob);
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _azuriteContainer.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
        await _azuriteContainer.StopAsync();
    }
}