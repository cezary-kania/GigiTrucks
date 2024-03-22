using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.Unsubscribe;

public record Unsubscribe(Guid SubscriberId) : IRequest;