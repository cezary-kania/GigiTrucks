using GigiTrucks.Services.Common.Identity;
using GigiTrucks.Services.Newsletter.Application.Exceptions;
using GigiTrucks.Services.Newsletter.Contracts.Events;
using GigiTrucks.Services.Newsletter.Domain.Entities;
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
        var existingSubscriber = await subscriberRepository.GetAsync(request.SubscriberId);
        if (existingSubscriber is not null && existingSubscriber.IsActive)
        {
            throw new AlreadySubscribedException(request.SubscriberId);
        }

        if (existingSubscriber is not null)
        {
            existingSubscriber.Subscribe();
            await subscriberRepository.UpdateAsync(existingSubscriber);
        }
        else
        {
            var newSubscriber = new Subscriber(request.SubscriberId, request.Email, true);
            await subscriberRepository.AddAsync(newSubscriber);
        }

        await publishEndpoint.Publish(
            new NewsletterSubscribed { SubscriberId = request.SubscriberId }, 
            cancellationToken);
    }
}