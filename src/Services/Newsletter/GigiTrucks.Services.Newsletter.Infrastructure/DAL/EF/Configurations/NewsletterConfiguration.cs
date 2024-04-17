using GigiTrucks.Services.Newsletter.Domain.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigiTrucks.Services.Newsletter.Infrastructure.DAL.EF.Configurations;

public class NewsletterConfiguration : IEntityTypeConfiguration<Domain.Entities.Newsletter>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Newsletter> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                x => new NewsletterId(x));
        
        builder.Property(x => x.Title)
            .IsRequired()
            .HasConversion<string>(
                x => x.Value,
                x => new NewsletterTitle(x));
        
        builder.Property(x => x.Content)
            .HasConversion<string>(
                x => x ?? string.Empty,
                x => new NewsletterContent(x));        
        
        builder.Property(x => x.PublishAt)
            .HasConversion<DateTimeOffset>(
                x => x ?? default,
                x => new PublishAt(x));

        builder.Property(x => x.Status)
            .IsRequired();
    }
}