using System.Collections.Immutable;
using System.Reflection;
using GigiTrucks.Services.Orders.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Orders.Infrastructure.DAL.EF;

public class OrdersDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<Order> OrderLines { get; set; }
    
    public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("orders");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}