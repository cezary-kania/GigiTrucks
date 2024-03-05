using GigiTrucks.Services.Users.Core.Entities;
using GigiTrucks.Services.Users.Core.Repositories;

namespace GigiTrucks.Services.Users.Core.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private static IList<User> _users = new List<User>();
    public Task<User?> GetAsync(string email) 
        => Task.FromResult(_users.FirstOrDefault(user => user.Email == email));

    public Task CreateAsync(User user)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }
}