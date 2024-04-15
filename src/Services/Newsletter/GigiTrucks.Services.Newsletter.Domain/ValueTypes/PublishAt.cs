namespace GigiTrucks.Services.Newsletter.Domain.ValueTypes;

public record struct PublishAt(DateTimeOffset Value)
{
    public static implicit operator DateTimeOffset(PublishAt publishAt)
        => publishAt.Value;
    
    public static implicit operator PublishAt(DateTimeOffset publishAt)
        => new(publishAt);
}