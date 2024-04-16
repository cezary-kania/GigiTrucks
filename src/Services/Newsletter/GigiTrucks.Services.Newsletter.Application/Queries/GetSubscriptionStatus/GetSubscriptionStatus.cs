using GigiTrucks.Services.Newsletter.Application.DTOs;
using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Queries.GetSubscriptionStatus;

public record GetSubscriptionStatus(Guid SubscriberId) : IRequest<SubscriptionStatusDto>;