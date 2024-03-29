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
            await sender.Send(command);
            return Results.NoContent();
        }).WithName("AddProduct");
    }
}