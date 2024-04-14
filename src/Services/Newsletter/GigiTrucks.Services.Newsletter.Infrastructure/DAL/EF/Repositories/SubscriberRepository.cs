using GigiTrucks.Services.Newsletter.Domain.Entities;
using GigiTrucks.Services.Newsletter.Domain.Repositories;
using GigiTrucks.Services.Newsletter.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Newsletter.Infrastructure.DAL.EF.Repositories;

internal sealed class SubscriberRepository(NewsletterDbContext dbContext) : ISubscriberRepository
{
    public async Task AddAsync(Subscriber subscriber)
    {
        await dbContext.Subscribers.AddAsync(subscriber);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Subscriber subscriber)
    {
        dbContext.Subscribers.Update(subscriber);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Subscriber?> GetAsync(SubscriberId subscriberId) 
        => await dbContext.Subscribers
            .Include(x => x.Subscription)
            .FirstOrDefaultAsync(x => x.Id == subscriberId);
}