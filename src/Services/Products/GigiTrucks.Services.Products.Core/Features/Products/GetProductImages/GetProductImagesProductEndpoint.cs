using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Products.GetProductImages;

public class GetProductImagesProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product/{productId:Guid}/images", async (
                [FromRoute] Guid productId,
                [FromServices] ISender sender) =>
                {
                    var result = await sender.Send(
                        new GetProductImagesProductQuery(productId));

                    return result.Match(
                        images => Results.Ok(images),
                        _ => Results.NotFound());
                })
            .WithName("GetProductImages")
            .WithTags("Product");
    }
}