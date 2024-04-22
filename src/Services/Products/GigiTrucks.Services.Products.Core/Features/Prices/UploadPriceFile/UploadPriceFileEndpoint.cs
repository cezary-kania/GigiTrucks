using Carter;
using GigiTrucks.Services.Products.Core.Features.Products.AddProductImage;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Prices.UploadPriceFile;

public class UploadPriceFileEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/priceList", async (
                [FromForm] IFormFile file,
                [FromServices] ISender sender) =>
            {
                var result = await sender.Send(new UploadPriceFileCommand(file));
                return result.Match(
                    _ => Results.Ok(),
                    error => Results.BadRequest(error.Value));
            })
            .WithName("UploadPriceFile")
            .WithTags("Price")
            .DisableAntiforgery();
    }
}