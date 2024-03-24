using GigiTrucks.Services.Products.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Products.Core.DAL.EF;

internal sealed class ProductsDbContext(DbContextOptions<ProductsDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Image> Images { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("products");
    }
}
