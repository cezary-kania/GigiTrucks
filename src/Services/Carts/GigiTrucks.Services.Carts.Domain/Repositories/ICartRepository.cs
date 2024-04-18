using GigiTrucks.Services.Carts.Domain.Entities;
using GigiTrucks.Services.Carts.Domain.ValueTypes;

namespace GigiTrucks.Services.Carts.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetAsync(CartId cartId);
    Task AddAsync(Cart cart);
    Task UpdateAsync(Cart cart);
    Task DeleteAsync(CartId cartId);
}