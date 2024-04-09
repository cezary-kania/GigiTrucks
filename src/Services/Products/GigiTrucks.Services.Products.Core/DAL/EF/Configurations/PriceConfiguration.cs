using GigiTrucks.Services.Products.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigiTrucks.Services.Products.Core.DAL.EF.Configurations;

public class PriceConfiguration : IEntityTypeConfiguration<Price>
{
    public void Configure(EntityTypeBuilder<Price> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.Prices)
            .HasForeignKey(x => x.ProductId);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(x => x.Currency)
            .IsRequired();
        
        builder.Property(x => x.ValidFrom)
            .IsRequired();
        
        builder.Property(x => x.ValidTo)
            .IsRequired();
    }
}