namespace GigiTrucks.Services.Users.Core.Auth;

public class JwtSettings
{
    public string Secret { get; init; } = null!;
    public int TokenLifetimeInMinutes { get; init; }
}