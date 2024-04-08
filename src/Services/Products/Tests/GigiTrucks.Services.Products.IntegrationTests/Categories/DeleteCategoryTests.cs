using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;

namespace GigiTrucks.Services.Products.IntegrationTests.Categories;

public class DeleteCategoryTests(ProductsApiFactory factory) : IClassFixture<ProductsApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task DeleteCategory_ShouldReturnNoConent_WhenCategoryWasRemoved()
    {
        // Arrange
        var createCategoryReponse = await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = "Car Parts" });
        var categoryIdAsString = await createCategoryReponse.Content.ReadAsStringAsync();
        var categoryId = Guid.Parse(categoryIdAsString.Replace("\"", string.Empty));
        
        // Act
        var response = await _client.DeleteAsync($"api/category/{categoryId}");
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NoContent);
    }    
    
    [Fact]
    public async Task DeleteCategory_ShouldReturnError_WhenDeletingCategoryWithSubCategories()
    {
        // Arrange
        var createCategoryReponse = await _client.PostAsJsonAsync(
            "api/category",
            new { Name = "Parts" });
        var categoryIdAsString = await createCategoryReponse.Content.ReadAsStringAsync();
        var categoryId = Guid.Parse(categoryIdAsString.Replace("\"", string.Empty));
        
        await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = "Truck Parts", ParentCategoryId = categoryId });
        
        // Act
        var response = await _client.DeleteAsync($"api/category/{categoryId}");
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }    
    
    [Fact]
    public async Task DeleteCategory_ShouldReturnNotFoundError_WhenCategoryDoesNotExist()
    {
        // Arrange
        var invalidCategoryId = Guid.NewGuid();
        
        // Act
        var response = await _client.DeleteAsync($"api/category/{invalidCategoryId}");
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }
}