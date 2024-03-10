using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;
using Xunit;

namespace GigiTrucks.Services.Users.Tests.Identity;

public class SignInTests
{
    private readonly HttpClient _client;
    public SignInTests()
    {
        _client = new UsersApiFactory()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var fakeTimeProvider = new FakeTimeProvider();
                    fakeTimeProvider.SetUtcNow(new DateTime(2024, 1, 1));
                    services.AddSingleton(fakeTimeProvider);
                });
            })
            .CreateClient();
    }

    [Fact]
    public async Task SignIn_ReturnsValidationError_WhenEmailIsEmpty()
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