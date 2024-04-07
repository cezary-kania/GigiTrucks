using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Products.CreateProduct;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/product", async (
            [FromBody] CreateProductCommand command,
            [FromServices] ISender sender) =>
            {
                var newProductId = Guid.NewGuid();
                var result = await sender.Send(command with { ProductId = newProductId });
                return result.Match(
                    _ => Results.CreatedAtRoute(
                        "GetProduct", 
                        new { productId = newProductId },
                        newProductId),
                    error => Results.BadRequest(error.Value));
            })
        .WithName("AddProduct")
        .WithTags("Product");
    }
}