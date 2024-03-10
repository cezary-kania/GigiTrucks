using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using Xunit;

namespace GigiTrucks.Services.Users.Tests.Identity;

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
}