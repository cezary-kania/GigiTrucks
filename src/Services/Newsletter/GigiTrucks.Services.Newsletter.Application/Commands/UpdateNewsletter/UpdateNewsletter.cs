using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.UpdateNewsletter;

public record UpdateNewsletter(
    Guid NewsletterId, 
    string Title,
    string Content) : IRequest;