using GigiTrucks.Services.Orders.Domain.Entities;
using GigiTrucks.Services.Orders.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigiTrucks.Services.Orders.Infrastructure.DAL.EF.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new OrderId(x));
        
        builder.Property(x => x.CustomerId)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new CustomerId(x));
        
        builder.Property(x => x.Status)
            .HasConversion<int>();
    }
}