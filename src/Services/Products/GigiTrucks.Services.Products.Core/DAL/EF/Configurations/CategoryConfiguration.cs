using GigiTrucks.Services.Products.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigiTrucks.Services.Products.Core.DAL.EF.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(x => x.ParentCategory)
            .WithMany(x => x.SubCategories)
            .HasForeignKey(x => x.ParentCategoryId);
    }
}