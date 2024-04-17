using FluentValidation;

namespace GigiTrucks.Services.Newsletter.Application.Commands.ScheduleNewsletter;

public class ScheduleNewsletterValidator : AbstractValidator<ScheduleNewsletter>
{
    public ScheduleNewsletterValidator()
    {
        RuleFor(x => x.NewsletterId)
            .NotEmpty();
        
        RuleFor(x => x.PublishAt)
            .NotEmpty();
    }
}