using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Categories.GetCategoryDetails;

public class GetCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/category/{categoryId:Guid}", async (
                [FromRoute] Guid categoryId,
                [FromServices] ISender sender) =>
            {
                var result = await sender.Send(new GetCategoryQuery(categoryId));
                return result.Match(
                    categoryDto => Results.Ok(categoryDto),
                    _ => Results.NotFound());
            })
        .WithName("GetCategory")
        .WithTags("Category");
    }
}