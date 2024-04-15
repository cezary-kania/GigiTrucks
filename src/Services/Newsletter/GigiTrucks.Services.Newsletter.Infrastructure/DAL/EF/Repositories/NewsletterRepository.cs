using GigiTrucks.Services.Newsletter.Domain.Repositories;
using GigiTrucks.Services.Newsletter.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Newsletter.Infrastructure.DAL.EF.Repositories;

internal sealed class NewsletterRepository(NewsletterDbContext dbContext) : INewsletterRepository
{
    public async Task AddAsync(Domain.Entities.Newsletter newsletter)
    {
        await dbContext.Newsletters.AddAsync(newsletter);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.Newsletter newsletter)
    {
        dbContext.Newsletters.Update(newsletter);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Domain.Entities.Newsletter?> GetAsync(NewsletterId newsletterId)
        => await dbContext.Newsletters.FirstOrDefaultAsync(x => x.Id == newsletterId);
}