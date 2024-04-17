using FluentValidation;

namespace GigiTrucks.Services.Newsletter.Application.Commands.UpdateNewsletter;

public class UpdateNewsletterValidator : AbstractValidator<UpdateNewsletter>
{
    public UpdateNewsletterValidator()
    {
        RuleFor(x => x.NewsletterId)
            .NotEmpty();
        
        RuleFor(x => x.Content)
            .NotEmpty();
        
        RuleFor(x => x.Title)
            .NotEmpty();
    }
}