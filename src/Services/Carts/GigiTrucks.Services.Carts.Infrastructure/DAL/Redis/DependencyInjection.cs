using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GigiTrucks.Services.Carts.Infrastructure.DAL.Redis;

public static class DependencyInjection
{
    public static IServiceCollection AddRedis(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<RedisSettings>(configuration.GetSection(nameof(RedisSettings)));
        var redisSettings = services.BuildServiceProvider()
            .GetRequiredService<IOptions<RedisSettings>>().Value;
        
        services.AddStackExchangeRedisCache(x => 
            x.Configuration = redisSettings.ConnectionString);
        return services;
    }
}