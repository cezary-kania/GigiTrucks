using System.Text.Json;
using GigiTrucks.Services.Carts.Domain.Entities;
using GigiTrucks.Services.Carts.Domain.Repositories;
using GigiTrucks.Services.Carts.Domain.ValueTypes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace GigiTrucks.Services.Carts.Infrastructure.DAL.Redis.Repositories;

public class CartRepository(IDistributedCache distributedCache, IOptions<RedisSettings> redisSettings) : ICartRepository
{
    private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(redisSettings.Value.CartTTL)
    };
    
    public async Task<Cart?> GetAsync(CustomerId customerId)
    {
        var serializedCart = await distributedCache.GetAsync(customerId.ToString());
        return JsonSerializer.Deserialize<Cart>(serializedCart);
    }

    public async Task AddAsync(Cart cart) => await SaveCart(cart);

    public async Task UpdateAsync(Cart cart) => await SaveCart(cart);

    public async Task DeleteAsync(CustomerId customerId) 
        => await distributedCache.RemoveAsync(customerId.ToString());

    private async Task SaveCart(Cart cart) 
        => await distributedCache.SetStringAsync(
            cart.CustomerId.ToString(), 
            JsonSerializer.Serialize(cart),
            _distributedCacheEntryOptions);
}