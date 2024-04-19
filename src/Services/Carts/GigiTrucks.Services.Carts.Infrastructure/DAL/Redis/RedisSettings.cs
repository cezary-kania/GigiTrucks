namespace GigiTrucks.Services.Carts.Infrastructure.DAL.Redis;

public class RedisSettings
{
    public string ConnectionString { get; init; } = null!;
    public int CartTTL { get; init; }
}