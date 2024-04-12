using GigiTrucks.Services.Newsletter.Domain.Entities;
using GigiTrucks.Services.Newsletter.Domain.ValueTypes;

namespace GigiTrucks.Services.Newsletter.Domain.Repositories;

public interface ISubscriberRepository
{
    Task AddAsync(Subscriber subscriber);
    Task UpdateAsync(Subscriber subscriber);
    Task<Subscriber?> GetAsync(SubscriberId subscriberId);
}