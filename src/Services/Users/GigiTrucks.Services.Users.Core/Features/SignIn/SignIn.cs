using GigiTrucks.Services.Users.Core.DTOs;
using MediatR;

namespace GigiTrucks.Services.Users.Core.Features.SignIn;

public record SignIn(string Email, string Password) : IRequest<JwtDto>;
