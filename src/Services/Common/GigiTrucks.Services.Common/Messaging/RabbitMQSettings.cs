namespace GigiTrucks.Services.Common.Messaging;

public record RabbitMQSettings
{
    public required string Host { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}