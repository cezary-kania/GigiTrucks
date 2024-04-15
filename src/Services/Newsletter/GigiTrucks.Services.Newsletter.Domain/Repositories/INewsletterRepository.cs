using GigiTrucks.Services.Newsletter.Domain.ValueTypes;

namespace GigiTrucks.Services.Newsletter.Domain.Repositories;

public interface INewsletterRepository
{
    Task AddAsync(Entities.Newsletter newsletter);
    Task UpdateAsync(Entities.Newsletter newsletter);
    Task<Entities.Newsletter?> GetAsync(NewsletterId newsletterId);
}