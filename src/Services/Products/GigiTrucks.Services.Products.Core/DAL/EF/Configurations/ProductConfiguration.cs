using GigiTrucks.Services.Products.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigiTrucks.Services.Products.Core.DAL.EF.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);        
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(4000);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products);
        
        builder.HasMany(x => x.Images);
    }
}