using Carter;
using GigiTrucks.Services.Products.Core.Features.Categories.CreateCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Categories.DeleteCategory;

public class DeleteCategoryEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/category/{categoryId:Guid}", async (
            [FromRoute] Guid categoryId,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new DeleteCategoryCommand(categoryId));
            return result.Match(
                _ => Results.NoContent(),
                _ => Results.NotFound(),
                error => Results.BadRequest(error.Value));
        }).WithName("DeleteCategory");
    }
}