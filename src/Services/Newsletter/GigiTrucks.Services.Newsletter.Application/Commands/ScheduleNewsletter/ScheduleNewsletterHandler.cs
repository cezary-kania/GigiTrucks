using MediatR;

namespace GigiTrucks.Services.Newsletter.Application.Commands.ScheduleNewsletter;

public class ScheduleNewsletterHandler : IRequestHandler<ScheduleNewsletter>
{
    public Task Handle(ScheduleNewsletter request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}