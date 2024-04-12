using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.Subscribe;

public record Subscribe(Guid SubscriberId, string Email) : IRequest;