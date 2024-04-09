using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace GigiTrucks.Services.Users.Tests.Integration.Identity;

public class SignUpTests(UsersApiFactory factory) : IClassFixture<UsersApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    
    [Fact]
    public async Task SignUp_ShouldReturnNoContentStatus_WhenCredentialsAreValid()
    {
        // Arrange
        var credentials = new { Email = "test@test.com", Password = "Test-Password" };
        
        // Act
        var response = await _client.PostAsJsonAsync("sign-up", credentials);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task SignUp_ShouldReturnInternalServerErrorStatus_WhenUserAlreadyExists()
    {
        // Arrange
        var credentials = new { Email = "test@test.com", Password = "Test-Password" };
        await _client.PostAsJsonAsync("sign-up", credentials);
        
        // Act
        var response = await _client.PostAsJsonAsync("sign-up", credentials);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.InternalServerError);
    }
}