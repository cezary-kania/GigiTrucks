using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Prices.GetPrices;

public class GetPricesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/price", async (
                    [AsParameters] GetPricesQuery query,
                    [FromServices] ISender sender)
                => Results.Ok(await sender.Send(query)))
            .WithName("GetPrices")
            .WithTags("Price");
    }
}