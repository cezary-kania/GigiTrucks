using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace GigiTrucks.Services.Products.Tests.Integration.Products;

public class CreateProductTests(ProductsApiFactory factory) : IClassFixture<ProductsApiFactory>
{
    private readonly HttpClient _client  = factory.CreateClient();
    
    [Fact]
    public async Task CreateProduct_ShouldReturnValidationError_WhenRequiredFieldsAreEmpty()
    {
        // Arrange
        var requestBody = new
        {
            CategoryId = Guid.Empty, Name = string.Empty, Description = string.Empty
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("api/category", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task CreateProduct_ShouldReturnValidationError_WhenCategoryDoesNotExist()
    {
        // Arrange
        var requestBody = new
        {
            CategoryId = Guid.NewGuid(), Name = "Oil Filter", Description = "Oil Filter X250"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("api/category", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task CreateProduct_ShouldCreateProduct_WhenInputIsValid()
    {
        var createCategoryReponse = await _client.PostAsJsonAsync(
            "api/category", new { Name = "Parts" });
        var categoryIdAsString = await createCategoryReponse.Content.ReadAsStringAsync();
        var categoryId = Guid.Parse(categoryIdAsString.Replace("\"", string.Empty));
        
        // Arrange
        var requestBody = new
        {
            CategoryId = categoryId, Name = "Oil Filter", Description = "Oil Filter X250"
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("api/category", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);
    }
}