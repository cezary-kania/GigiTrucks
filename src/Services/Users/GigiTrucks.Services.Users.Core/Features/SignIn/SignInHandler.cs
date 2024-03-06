using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GigiTrucks.Services.Users.Core.Auth;
using GigiTrucks.Services.Users.Core.DTOs;
using GigiTrucks.Services.Users.Core.Entities;
using GigiTrucks.Services.Users.Core.Exceptions.Auth;
using GigiTrucks.Services.Users.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GigiTrucks.Services.Users.Core.Features.SignIn;

public class SignInHandler(
    IPasswordHasher<User> passwordHasher,
    IUserRepository userRepository,
    IOptions<JwtSettings> jwtOptions,
    TimeProvider timeProvider) : IRequestHandler<SignIn, JwtDto>
{
    public async Task<JwtDto> Handle(SignIn request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(request.Email);
        if (user is null || !IsPasswordValid(request.Password, user.Password))
        {
            throw new InvalidCredentialsException();
        }

        return GenerateToken(user);
    }

    private bool IsPasswordValid(string providedPassword, string hashedPassword)
        => passwordHasher.VerifyHashedPassword(new(), hashedPassword, providedPassword) ==
           PasswordVerificationResult.Success;
    
    private JwtDto GenerateToken(User user)
    {
        var jwtSettings = jwtOptions.Value;
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var securityToken = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: timeProvider.GetUtcNow().AddMinutes(jwtSettings.TokenLifetimeInMinutes).UtcDateTime
        );

        return new JwtDto(
            new JwtSecurityTokenHandler().WriteToken(securityToken), 
            user.Id);
    }
}