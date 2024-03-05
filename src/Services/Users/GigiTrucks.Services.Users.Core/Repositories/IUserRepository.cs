using GigiTrucks.Services.Users.Core.Entities;

namespace GigiTrucks.Services.Users.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetAsync(string email);
    Task CreateAsync(User user);
}