namespace GigiTrucks.Services.Orders.Domain.Entities.Common;

public abstract class AuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
    public Guid? LastModifiedBy { get; set; }
}