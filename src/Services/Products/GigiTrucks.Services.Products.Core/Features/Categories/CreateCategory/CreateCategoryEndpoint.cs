using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Categories.CreateCategory;

public class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/category", async (
            [FromBody] CreateCategoryCommand command,
            [FromServices] ISender sender) =>
            {
                var newCategoryId = Guid.NewGuid();
                var result = await sender.Send(command with { CategoryId = newCategoryId });
                return result.Match(
                    _ => Results.CreatedAtRoute("GetCategory", 
                        new { categoryId = newCategoryId }, 
                        newCategoryId),
                    error => Results.BadRequest(error.Value));
            })
        .WithName("CreateCategory")
        .WithTags("Category");
    }
}