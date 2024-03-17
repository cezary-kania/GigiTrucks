using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace GigiTrucks.Services.Orders.Application.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid? UserId =>
        Guid.TryParse(
            httpContextAccessor.HttpContext?
                .User?
                .Claims?
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?
                .Value, out var userId
        ) ? userId : null;
}