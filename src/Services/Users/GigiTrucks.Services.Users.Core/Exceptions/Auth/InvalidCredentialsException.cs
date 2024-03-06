namespace GigiTrucks.Services.Users.Core.Exceptions.Auth;

public class InvalidCredentialsException() 
    : ApplicationException($"Email or password is invalid.");