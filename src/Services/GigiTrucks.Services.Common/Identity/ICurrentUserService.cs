namespace GigiTrucks.Services.Common.Identity;

public interface ICurrentUserService
{
    public Guid? UserId { get; }
}