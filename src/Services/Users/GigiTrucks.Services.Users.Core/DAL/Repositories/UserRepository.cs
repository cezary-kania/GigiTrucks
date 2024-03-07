using GigiTrucks.Services.Users.Core.DAL.EntityFramework;
using GigiTrucks.Services.Users.Core.Entities;
using GigiTrucks.Services.Users.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Users.Core.DAL.Repositories;

internal sealed class UserRepository(UsersDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetAsync(string email)
        => await dbContext.Users.SingleOrDefaultAsync(user => user.Email == email);

    public async Task CreateAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
}