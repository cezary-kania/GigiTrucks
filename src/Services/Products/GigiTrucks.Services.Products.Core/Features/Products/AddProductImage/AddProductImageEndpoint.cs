using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Products.AddProductImage;

public class AddProductImageEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product/{productId:Guid}/image", async (
            [FromRoute] Guid productId,
            [FromForm] IFormFile file,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new AddProductImageCommand(productId, file));
            return result.Match(
                _ => Results.Created(),
                _ => Results.NotFound(),
                error => Results.BadRequest(error.Value));
        })
            .WithName("AddProductImage")
            .DisableAntiforgery();
    }
}