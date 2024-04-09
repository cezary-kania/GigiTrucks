using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using GigiTrucks.Services.Users.Core.DTOs;
using Xunit;

namespace GigiTrucks.Services.Users.Tests.Integration.Identity;

public class SignInTests(UsersApiFactory factory) : IClassFixture<UsersApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task SignIn_ShouldReturnValidationError_WhenEmailIsEmpty()
    {
        // Arrange
        var requestBody = new { Email = string.Empty, Password = "Test-Password" };
        
        // Act
        var response = await _client.PostAsJsonAsync("sign-in", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task SignIn_ShouldReturnValidationError_WhenPasswordIsEmpty()
    {
        // Arrange
        var requestBody = new { Email = "test@test.com", Password = string.Empty };
        
        // Act
        var response = await _client.PostAsJsonAsync("sign-in", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task SignIn_ShouldReturnOkStatus_WhenCredentialsAreValid()
    {
        // Arrange
        var credentials = new { Email = "test@test.com", Password = "Test-Password" };
        await _client.PostAsJsonAsync("sign-up", credentials);
        
        // Act
        var response = await _client.PostAsJsonAsync("sign-in", credentials);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);

        var responseString = await response.Content.ReadAsStringAsync();
        var authResult = JsonSerializer.Deserialize<JwtDto>(responseString, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        authResult.Should().NotBeNull();
        authResult?.AccessToken.Should().NotBeEmpty();
        authResult?.UserId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task SignIn_ShouldReturnInternalServerErrorStatus_WhenUserDoesNotExists()
    {
        // Arrange
        var credentials = new { Email = "invalid@test.com", Password = "Test-Password" };
        
        // Act
        var response = await _client.PostAsJsonAsync("sign-in", credentials);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.InternalServerError);
    }
}