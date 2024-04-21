using System.Text.Json;
using GigiTrucks.Services.Carts.Domain.Entities;
using GigiTrucks.Services.Carts.Domain.Enums;
using GigiTrucks.Services.Carts.Domain.Repositories;
using GigiTrucks.Services.Carts.Domain.ValueTypes;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace GigiTrucks.Services.Carts.Infrastructure.DAL.Redis.Repositories;

public class CartRepository(IDistributedCache distributedCache, IOptions<RedisSettings> redisSettings) : ICartRepository
{
    private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(redisSettings.Value.CartTTL)
    };
    
    public async Task<Cart?> GetAsync(CustomerId customerId) 
        => JsonSerializer.Deserialize<Cart>(await distributedCache.GetAsync(customerId.ToString()));

    public async Task<Cart?> GetByIdAsync(CartId cartId) 
        => JsonSerializer.Deserialize<Cart>(await distributedCache.GetAsync(cartId.ToString()));

    public async Task PersistAsync(Cart cart)
    {
        await distributedCache.RemoveAsync(cart.CustomerId.ToString());
        await distributedCache.SetStringAsync(cart.Id.ToString(), JsonSerializer.Serialize(cart));
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