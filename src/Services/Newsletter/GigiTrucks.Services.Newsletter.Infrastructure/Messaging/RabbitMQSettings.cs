namespace GigiTrucks.Services.Newsletter.Infrastructure.Messaging;

public class RabbitMQSettings
{
    public string Host { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
}