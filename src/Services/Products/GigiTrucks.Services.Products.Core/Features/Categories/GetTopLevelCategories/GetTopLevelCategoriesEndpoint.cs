using Carter;
using GigiTrucks.Services.Products.Core.Features.Categories.DeleteCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace GigiTrucks.Services.Products.Core.Features.Categories.GetTopLevelCategories;

public class GetTopLevelCategoriesEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/category", async ([FromServices] ISender sender) 
                => Results.Ok(await sender.Send(new GetTopLevelCategoriesQuery())))
            .WithName("GetTopLevelCategories")
            .WithTags("Category");
    }
}