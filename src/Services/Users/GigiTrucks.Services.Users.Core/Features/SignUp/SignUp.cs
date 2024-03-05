using MediatR;

namespace GigiTrucks.Services.Users.Core.Features.SignUp;

public record SignUp(string Email, string Password) : IRequest;
