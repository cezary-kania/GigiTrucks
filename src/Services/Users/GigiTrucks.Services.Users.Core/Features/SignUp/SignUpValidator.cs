using FluentValidation;

namespace GigiTrucks.Services.Users.Core.Features.SignUp;

public class SignUpValidator : AbstractValidator<SignUp>
{
    public SignUpValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .WithName("Email can't be empty");
        
        RuleFor(command => command.Password)
            .NotEmpty()
            .WithName("Password can't be empty");
    }
}