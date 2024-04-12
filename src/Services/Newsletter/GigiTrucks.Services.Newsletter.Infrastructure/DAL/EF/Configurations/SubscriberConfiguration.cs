using GigiTrucks.Services.Newsletter.Domain.Entities;
using GigiTrucks.Services.Newsletter.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigiTrucks.Services.Newsletter.Infrastructure.DAL.EF.Configurations;

public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
{
    public void Configure(EntityTypeBuilder<Subscriber> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Email)
            .IsUnique();
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value, 
                x => new SubscriberId(x));
        
        builder.Property(x => x.Email)
            .IsRequired()
            .HasConversion(x => x.Value, 
                x => new Email(x));        
        
        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasConversion(x => x.Value, 
                x => new SubscriptionStatus(x));
    }
}