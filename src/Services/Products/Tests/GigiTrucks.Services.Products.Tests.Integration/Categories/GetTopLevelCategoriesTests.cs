using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using GigiTrucks.Services.Products.Core.DTOs.Categories;
using Xunit;

namespace GigiTrucks.Services.Products.Tests.Integration.Categories;

public class GetTopLevelCategoriesTests(ProductsApiFactory factory) : IClassFixture<ProductsApiFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    
    [Fact]
    public async Task GetTopLevelCategories_ShouldReturnCategoriesWithNoParent()
    {
        // Arrange
        const string categoryName = "Parts";
        var createCategoryReponse = await _client.PostAsJsonAsync(
            "api/category", 
            new { Name = categoryName });
        var categoryIdAsString = await createCategoryReponse.Content.ReadAsStringAsync();
        var categoryId = Guid.Parse(categoryIdAsString.Replace("\"", string.Empty));        
        
        var subCategoryNames = new List<string>{ "Truck Parts", "Car Parts" };
        foreach (var subCategoryName in subCategoryNames)
        {
            await _client.PostAsJsonAsync(
                "api/category", 
                new { Name = subCategoryName, ParentCategoryId = categoryId });
        }
        
        // Act
        var response = await _client.GetAsync($"api/category");
        
        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
        
        var topLevelCategories = await response.Content.ReadFromJsonAsync<IList<CategoryWithoutProductsDto>>();


        topLevelCategories
            .Should()
            .HaveCount(1)
            .And
            .ContainSingle(x => x.Name == categoryName);
        
        topLevelCategories[0].SubCategories
            .Should()
            .HaveCount(2)
            .And
            .ContainSingle(x => x.Name == subCategoryNames[0])
            .And
            .ContainSingle(x => x.Name == subCategoryNames[1]);
    }
}