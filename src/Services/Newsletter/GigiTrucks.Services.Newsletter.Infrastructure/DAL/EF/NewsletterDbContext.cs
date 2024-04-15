using System.Reflection;
using GigiTrucks.Services.Newsletter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GigiTrucks.Services.Newsletter.Infrastructure.DAL.EF;


internal sealed class NewsletterDbContext(DbContextOptions<NewsletterDbContext> options) : DbContext(options)
{
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Domain.Entities.Newsletter> Newsletters { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("newsletter");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
