namespace GigiTrucks.Services.Newsletter.Domain.ValueTypes;

public record struct NewsletterContent(string Value)
{
    public static implicit operator string(NewsletterContent content)
        => content.Value;
    
    public static implicit operator NewsletterContent(string content)
        => new(content);
}