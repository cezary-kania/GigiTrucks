using GigiTrucks.Services.Common.Domain.Entities;
using GigiTrucks.Services.Common.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GigiTrucks.Services.Orders.Infrastructure.DAL.EF.Interceptors;

public class AuditableEntityInterceptor(
    ICurrentUserService currentUserService,
    TimeProvider timeProvider) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is not null)
        {
            UpdateAuditableProperties(eventData.Context);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditableProperties(DbContext dbContext)
    {
        foreach (var entry in dbContext.ChangeTracker.Entries<AuditableEntity>())
        {
            if (IsEntryAddedOrUpdated(entry))
            {
                continue;
            }
            var userId = currentUserService.UserId ?? Guid.Empty;
            var utcNow = timeProvider.GetUtcNow();
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = utcNow;
                entry.Entity.CreatedBy = userId;
            }
            entry.Entity.LastModifiedAt = utcNow;
            entry.Entity.LastModifiedBy = userId;
        }
    }

    private static bool IsEntryAddedOrUpdated(EntityEntry<AuditableEntity> entry) 
        => entry.State is not (EntityState.Added or EntityState.Modified);
}
