using GigiTrucks.Services.Orders.Domain.Entities;
using GigiTrucks.Services.Orders.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigiTrucks.Services.Orders.Infrastructure.DAL.EF.Configurations;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new OrderLineId(x));

        builder.Property(x => x.ProductId)
            .IsRequired()
            .HasConversion(x => x.Value,
                x => new ProductId(x));

        builder.Property(x => x.UnitPrice)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();
    }
}