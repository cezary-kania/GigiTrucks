namespace GigiTrucks.Services.Newsletter.Domain.ValueTypes;

public record struct Email(Guid Value)
{
    public static implicit operator Guid(Email email)
        => email.Value;
    
    public static implicit operator Email(Guid email)
        => new(email);
}