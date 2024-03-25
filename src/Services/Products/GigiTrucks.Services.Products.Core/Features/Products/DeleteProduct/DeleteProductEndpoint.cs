using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Products.DeleteProduct;

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/product/{productId:Guid}", async (
            [FromRoute] Guid productId,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(productId));
            
            return result.Match(
                _ => Results.NoContent(),
                _ => Results.NotFound());
        }).WithName("DeleteProduct");
    }
}