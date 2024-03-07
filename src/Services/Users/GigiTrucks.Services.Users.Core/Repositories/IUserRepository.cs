using GigiTrucks.Services.Users.Core.Entities;

namespace GigiTrucks.Services.Users.Core.Repositories;

internal interface IUserRepository
{
    Task<User?> GetAsync(string email);
    Task CreateAsync(User user);
}