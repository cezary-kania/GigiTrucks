using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Categories.UpdateCategory;

public class UpdateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/category/{categoryId:Guid}", async (
                [FromRoute] Guid categoryId,
                [FromBody] UpdateCategoryCommand command,
                [FromServices] ISender sender) =>
                {
                    var result = await sender.Send(command with { CategoryId = categoryId });
                    return result.Match(
                        _ => Results.Ok(),
                        _ => Results.NotFound(),
                        error => Results.BadRequest(error.Value));
                })
            .WithName("UpdateCategory")
            .WithTags("Category");
    }
}