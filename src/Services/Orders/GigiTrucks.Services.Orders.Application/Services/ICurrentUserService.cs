namespace GigiTrucks.Services.Orders.Application.Services;

public interface ICurrentUserService
{
    public Guid? UserId { get; }
}