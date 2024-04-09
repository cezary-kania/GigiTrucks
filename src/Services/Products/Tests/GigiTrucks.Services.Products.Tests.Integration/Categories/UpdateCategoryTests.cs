using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using GigiTrucks.Services.Products.Core.DTOs.Categories;
using Xunit;

namespace GigiTrucks.Services.Products.Tests.Integration.Categories;

public class UpdateCategoryTests(ProductsApiFactory factory) : IClassFixture<ProductsApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    [Fact]
    public async Task UpdateCategory_ShouldChangeCategoryDetails_WhenNewPropertiesAreValid()
    {
        // Arrange
        const string categoryName = "Parts";
        var createParentCategoryReponse = await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = categoryName });
        var parentCategoryId = await GetCategoryIdAsync(createParentCategoryReponse);        
        
        const string subCategoryName = "Truck Parts";
        var createSubCategoryReponse = await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = subCategoryName });
        var subCategoryId = await GetCategoryIdAsync(createSubCategoryReponse);
        const string newCategoryName = "Car Parts"; 
        
        // Act
        var response = await _client.PutAsJsonAsync(
            $"api/category/{subCategoryId}", 
            new { Name = newCategoryName, ParentCategoryId = parentCategoryId });
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        
        var getParentCategoryResponse = await _client.GetAsync($"api/category/{parentCategoryId}");
        var responseString = await getParentCategoryResponse.Content.ReadAsStringAsync();
        var categoryDto = JsonSerializer.Deserialize<CategoryDto>(responseString, _serializerOptions)!;

        categoryDto.SubCategories
            .Should()
            .HaveCount(1)
            .And
            .ContainSingle(x => x.Name == newCategoryName);
    }
    
    [Fact]
    public async Task UpdateCategory_ShouldReturnBadRequestError_WhenNameIsEmpty()
    {
        // Arrange
        const string categoryName = "Wheels";
        var createParentCategoryReponse = await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = categoryName });
        var categoryId = await GetCategoryIdAsync(createParentCategoryReponse);        
        
        // Act
        var response = await _client.PutAsJsonAsync(
            $"api/category/{categoryId}", 
            new { Name = string.Empty });
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task UpdateCategory_ShouldReturnBadRequestError_WhenParentCategoryDoesntExist()
    {
        // Arrange
        const string categoryName = "Oils";
        var createParentCategoryReponse = await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = categoryName });
        var categoryId = await GetCategoryIdAsync(createParentCategoryReponse);        
        
        // Act
        var response = await _client.PutAsJsonAsync(
            $"api/category/{categoryId}", 
            new { Name = categoryName, ParentCategoryId = Guid.NewGuid() });
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task UpdateCategory_ShouldReturnNotFoundError_WhenCategoryDoesntExist()
    {
        // Arrange
        const string categoryName = "Parts";
        var fakeCategoryId = Guid.NewGuid();
        
        // Act
        var response = await _client.PutAsJsonAsync(
            $"api/category/{fakeCategoryId}", 
            new { Name = categoryName, ParentCategoryId = fakeCategoryId });
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }

    private async Task<Guid> GetCategoryIdAsync(HttpResponseMessage categoryReponse)
    {
        var categoryIdAsString = await categoryReponse.Content.ReadAsStringAsync();
        return Guid.Parse(categoryIdAsString.Replace("\"", string.Empty));  
    }
}