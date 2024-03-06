namespace GigiTrucks.Services.Users.Core.DTOs;

public record JwtDto(
    string AccessToken,
    Guid UserId);