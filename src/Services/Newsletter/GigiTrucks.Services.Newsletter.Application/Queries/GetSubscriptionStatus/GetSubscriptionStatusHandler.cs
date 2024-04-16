using GigiTrucks.Services.Newsletter.Application.DTOs;
using GigiTrucks.Services.Newsletter.Domain.Entities;
using GigiTrucks.Services.Newsletter.Domain.Repositories;
using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Queries.GetSubscriptionStatus;

public class GetSubscriptionStatusHandler(ISubscriberRepository subscriberRepository) 
    : IRequestHandler<GetSubscriptionStatus, SubscriptionStatusDto>
{
    public async Task<SubscriptionStatusDto> Handle(GetSubscriptionStatus request, CancellationToken cancellationToken)
    {
        var subscriber = await subscriberRepository.GetAsync(request.SubscriberId);
        
        return new SubscriptionStatusDto(
            request.SubscriberId, 
            subscriber?.Subscription?.IsActive.Value ?? false);
    }
}