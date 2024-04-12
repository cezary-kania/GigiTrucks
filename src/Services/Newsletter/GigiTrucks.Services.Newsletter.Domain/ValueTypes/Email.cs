namespace GigiTrucks.Services.Newsletter.Domain.ValueTypes;

public record struct Email(string Value)
{
    public static implicit operator string(Email email)
        => email.Value;
    
    public static implicit operator Email(string email)
        => new(email);
}