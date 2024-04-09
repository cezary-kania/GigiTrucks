using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace GigiTrucks.Services.Products.Tests.Integration.Categories;

public class CreateCategoryTests(ProductsApiFactory factory) : IClassFixture<ProductsApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    
    [Fact]
    public async Task CreateCategory_ShouldReturnValidationError_WhenNameIsEmpty()
    {
        // Arrange
        var requestBody = new { Name = string.Empty };
        
        // Act
        var response = await _client.PostAsJsonAsync("api/category", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }    
    
    [Fact]
    public async Task CreateCategory_ShouldReturnError_WhenNameWasAlreadyUsed()
    {
        // Arrange
        var requestBody = new { Name = "Parts" };
        await _client.PostAsJsonAsync("api/category", requestBody);
        
        // Act
        var response = await _client.PostAsJsonAsync("api/category", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }    
    
    [Fact]
    public async Task CreateCategory_ShouldReturnCreated_WhenNameWasProvided()
    {
        // Arrange
        var requestBody = new { Name = "New Parts" };
        
        // Act
        var response = await _client.PostAsJsonAsync("api/category", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);
    }    
    
    [Fact]
    public async Task CreateCategory_ShouldReturnError_WhenParentCategoryDoesntExist()
    {
        // Arrange
        var requestBody = new { Name = "Truck Parts", ParentCategoryId = Guid.NewGuid() };
        
        // Act
        var response = await _client.PostAsJsonAsync("api/category", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }    
    
    [Fact]
    public async Task CreateCategory_ShouldCreateSubCategory_WhenParentCategoryIdIsValid()
    {
        // Arrange
        var createParentCategoryResponse = await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = "Car Parts" });

        var parentCategoryIdAsString = await createParentCategoryResponse.Content.ReadAsStringAsync();
        var parentCategoryId = Guid.Parse(parentCategoryIdAsString.Replace("\"", string.Empty));
        
        var requestBody = new { Name = "Oil Filters", ParentCategoryId = parentCategoryId };
        
        // Act
        var response = await _client.PostAsJsonAsync("api/category", requestBody);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);
    }
}