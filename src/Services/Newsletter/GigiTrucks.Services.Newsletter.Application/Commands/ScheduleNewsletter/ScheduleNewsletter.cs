using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.ScheduleNewsletter;

public record ScheduleNewsletter(Guid NewsletterId) : IRequest;