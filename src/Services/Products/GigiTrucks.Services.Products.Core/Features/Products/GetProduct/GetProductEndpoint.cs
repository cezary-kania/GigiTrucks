using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Products.GetProduct;

public class GetProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/product/{productId:Guid}", async (
                [FromRoute] Guid productId,
                [FromServices] ISender sender) =>
            {
                var result = await sender.Send(new GetProductQuery(productId));
                return result.Match(
                    product => Results.Ok(product),
                    _ => Results.NotFound());
            })
           .WithName("GetProduct")
           .WithTags("Product");
    }
}