using GigiTrucks.Services.Users.Core.Entities;
using GigiTrucks.Services.Users.Core.Exceptions.Auth;
using GigiTrucks.Services.Users.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GigiTrucks.Services.Users.Core.Features.SignUp;

internal sealed class SignUpHandler(IPasswordHasher<User> passwordHasher, IUserRepository userRepository) : IRequestHandler<SignUp>
{
    public async Task Handle(SignUp request, CancellationToken cancellationToken)
    {
        if (await userRepository.GetAsync(request.Email) is not null)
        {
            throw new EmailAlreadyUsedException(request.Email);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Password = passwordHasher.HashPassword(new (), request.Password)
        };
        await userRepository.CreateAsync(user);
    }
}