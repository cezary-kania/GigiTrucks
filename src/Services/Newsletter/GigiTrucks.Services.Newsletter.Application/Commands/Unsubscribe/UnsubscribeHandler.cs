using GigiTrucks.Services.Newsletter.Application.Exceptions;
using GigiTrucks.Services.Newsletter.Contracts.Events;
using GigiTrucks.Services.Newsletter.Domain.Repositories;
using MassTransit;
using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.Unsubscribe;

public class UnsubscribeHandler(
    ISubscriberRepository subscriberRepository,
    IPublishEndpoint publishEndpoint) : IRequestHandler<Unsubscribe>
{
    public async Task Handle(Unsubscribe request, CancellationToken cancellationToken)
    {
        var subscriber = await subscriberRepository.GetAsync(request.SubscriberId);
        if (subscriber is null || !subscriber.IsActive)
        {
            throw new NotSubscribedException(request.SubscriberId);
        }
        
        subscriber.Unsubscribe();
        await subscriberRepository.UpdateAsync(subscriber);
        
        await publishEndpoint.Publish(
            new NewsletterUnsubscribed { SubscriberId = request.SubscriberId }, 
            cancellationToken);
    }
}