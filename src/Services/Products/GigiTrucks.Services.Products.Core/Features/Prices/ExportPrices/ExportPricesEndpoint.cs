using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Prices.ExportPrices;

public class ExportPricesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/price/export", async (
                [FromBody] ExportPricesQuery query,
                [FromServices] ISender sender) 
                =>
            {
                var result = await sender.Send(query);
                return Results.File(
                    result.Content,
                    result.ContentType,
                    result.ExportName);
            })
            .WithName("ExportPrices")
            .WithTags("Price");
    }
}