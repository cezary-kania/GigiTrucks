using GigiTrucks.Services.Newsletter.Domain.Entities;
using GigiTrucks.Services.Newsletter.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigiTrucks.Services.Newsletter.Infrastructure.DAL.EF.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value, 
                x => new SubscriptionId(x));
        
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasConversion(x => x.Value, 
                x => new SubscriptionStatus(x));
    }
}