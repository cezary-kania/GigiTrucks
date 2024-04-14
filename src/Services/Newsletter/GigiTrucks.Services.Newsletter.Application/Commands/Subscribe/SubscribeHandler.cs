using GigiTrucks.Services.Common.Identity;
using GigiTrucks.Services.Newsletter.Application.Exceptions;
using GigiTrucks.Services.Newsletter.Contracts.Events;
using GigiTrucks.Services.Newsletter.Domain.Entities;
using GigiTrucks.Services.Newsletter.Domain.Exceptions;
using GigiTrucks.Services.Newsletter.Domain.Repositories;
using MassTransit;
using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.Subscribe;

public class SubscribeHandler(
    ISubscriberRepository subscriberRepository,
    IPublishEndpoint publishEndpoint) : IRequestHandler<Subscribe>
{
    public async Task Handle(Subscribe request, CancellationToken cancellationToken)
    {
        var subscriber = await subscriberRepository.GetAsync(request.SubscriberId);
        if (subscriber is null)
        {
            subscriber = new Subscriber(request.SubscriberId, request.Email);
            await subscriberRepository.AddAsync(subscriber);
        }
        
        subscriber.Subscribe();
        await subscriberRepository.UpdateAsync(subscriber);

        await publishEndpoint.Publish(
            new NewsletterSubscribed { SubscriberId = request.SubscriberId }, 
            cancellationToken);
    }
}