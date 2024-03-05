namespace GigiTrucks.Services.Users.Core.Exceptions.Auth;

public class EmailAlreadyUsedException(string email) 
    : ApplicationException($"Email: \"{email}\" was already used.");