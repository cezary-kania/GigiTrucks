using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Products.UpdateProduct;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/product", async (
            [FromBody] UpdateProductCommand command,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.Match(
                _ => Results.NoContent(),
                _ => Results.NotFound(),
                error => Results.BadRequest(error.Value));
        }).WithName("UpdateProduct");
    }
}