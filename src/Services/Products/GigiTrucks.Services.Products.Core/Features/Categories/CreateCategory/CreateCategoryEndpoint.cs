using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Categories.CreateCategory;

public class CreateCategoryEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/category", async (
            [FromBody] CreateCategoryCommand command,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.Match(
                _ => Results.Created(),
                error => Results.BadRequest(error.Value));
        }).WithName("CreateCategory");
    }
}