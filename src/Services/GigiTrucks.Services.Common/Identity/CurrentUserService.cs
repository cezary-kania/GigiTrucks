using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace GigiTrucks.Services.Common.Identity;

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
    
    public string? UserEmail =>
        httpContextAccessor.HttpContext?
            .User?
            .Claims?
            .FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?
            .Value;
}