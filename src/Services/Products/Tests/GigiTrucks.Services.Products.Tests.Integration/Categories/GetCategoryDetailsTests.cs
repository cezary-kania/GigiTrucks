using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using GigiTrucks.Services.Products.Core.DTOs.Categories;
using Xunit;

namespace GigiTrucks.Services.Products.Tests.Integration.Categories;

public class GetCategoryDetailsTests(ProductsApiFactory factory) : IClassFixture<ProductsApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();

    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    [Fact]
    public async Task GetCategory_ShouldReturnCategoryDetails_WhenCategoryExists()
    {
        // Arrange
        const string categoryName = "Parts";
        var createCategoryReponse = await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = categoryName });
        var categoryIdAsString = await createCategoryReponse.Content.ReadAsStringAsync();
        var categoryId = Guid.Parse(categoryIdAsString.Replace("\"", string.Empty));        
        
        const string subCategoryName = "Truck Parts";
        await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = subCategoryName, ParentCategoryId = categoryId });        
        
        const string productName = "Oil filter";
        await _client.PostAsJsonAsync(
            "api/product", 
            new { Name = productName, Description = "Sample description", CategoryId = categoryId });
        
        // Act
        var response = await _client.GetAsync($"api/category/{categoryId}");
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        
        var responseString = await response.Content.ReadAsStringAsync();
        var categoryDto = JsonSerializer.Deserialize<CategoryDto>(responseString, _serializerOptions)!;

        categoryDto.Name
            .Should()
            .Be(categoryName);
        
        categoryDto.Products
            .Should()
            .HaveCount(1)
            .And
            .ContainSingle(x => x.Name == productName);
        
        categoryDto.SubCategories
            .Should()
            .HaveCount(1)
            .And
            .ContainSingle(x => x.Name == subCategoryName);
    }
    
    [Fact]
    public async Task GetCategory_ShouldReturnNotFoundErro_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        
        // Act
        var response = await _client.GetAsync($"api/category/{categoryId}");
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }
}