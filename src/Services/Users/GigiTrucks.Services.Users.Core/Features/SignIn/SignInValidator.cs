using FluentValidation;

namespace GigiTrucks.Services.Users.Core.Features.SignIn;

public class SignInValidator : AbstractValidator<SignIn>
{
    public SignInValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithName("Email can't be empty");
        
        RuleFor(command => command.Password)
            .NotEmpty()
            .WithName("Password can't be empty");
    }
}