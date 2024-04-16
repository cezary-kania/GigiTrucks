using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.CreateNewsletter;

public record CreateNewsletter(Guid NewsletterId) : IRequest;