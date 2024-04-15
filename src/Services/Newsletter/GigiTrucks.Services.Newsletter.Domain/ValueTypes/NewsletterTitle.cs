namespace GigiTrucks.Services.Newsletter.Domain.ValueTypes;

public record struct NewsletterTitle(string Value)
{
    public static implicit operator string(NewsletterTitle title)
        => title.Value;
    
    public static implicit operator NewsletterTitle(string title)
        => new(title);
}