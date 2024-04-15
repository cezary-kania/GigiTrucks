namespace GigiTrucks.Services.Newsletter.Domain.ValueTypes;

public record struct NewsletterId(Guid Value)
{
    public static implicit operator Guid(NewsletterId newsletterId)
        => newsletterId.Value;

    public static implicit operator NewsletterId(Guid newsletterId)
        => new(newsletterId);
}