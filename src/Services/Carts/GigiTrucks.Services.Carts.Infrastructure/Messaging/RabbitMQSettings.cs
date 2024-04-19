namespace GigiTrucks.Services.Carts.Infrastructure.Messaging;

public class RabbitMQSettings
{
    public string Host { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}