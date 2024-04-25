using GigiTrucks.Services.Carts.Api.Requests;
using GigiTrucks.Services.Carts.Application;
using GigiTrucks.Services.Carts.Application.Commands.CreateCart;
using GigiTrucks.Services.Carts.Application.Commands.DeleteCart;
using GigiTrucks.Services.Carts.Application.Commands.SubmitCart;
using GigiTrucks.Services.Carts.Application.Commands.UpdateCartItems;
using GigiTrucks.Services.Carts.Application.Queries.GetCart;
using GigiTrucks.Services.Carts.Infrastructure;
using GigiTrucks.Services.Common.Identity;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Carts");

var cartGroup = app.MapGroup("api/cart")
    .WithTags("Cart");

cartGroup.MapGet("/", async (
    [FromServices] ICurrentUserService currentUserService,
    [FromServices] ISender sender) =>
    {
        var customerId = currentUserService.UserId;
        return  Results.Ok(await sender.Send(new GetCart(customerId!.Value)));
    })
    .WithName("GetCart");

cartGroup.MapPost("/", async (
    [FromBody] CreateCartRequest request,
    [FromServices] ICurrentUserService currentUserService,
    [FromServices] ISender sender) =>
    {
        var customerId = currentUserService.UserId;
        var createCartCommand = request.Adapt<CreateCart>() with { CustomerId = customerId!.Value };
        await sender.Send(createCartCommand);
        return Results.CreatedAtRoute("GetCart");
    })
    .WithName("CreateCart");

cartGroup.MapPut("/", async (
    [FromBody] UpdateCartRequest request,
    [FromServices] ICurrentUserService currentUserService,
    [FromServices] ISender sender) =>
    {
        var customerId = currentUserService.UserId;
        var updateCartCommand = request.Adapt<UpdateCartItems>() with { CustomerId = customerId!.Value };
        await sender.Send(updateCartCommand);
        return Results.NoContent();
    })
    .WithName("UpdateCart");

cartGroup.MapPatch("/submit", async (
    [FromServices] ICurrentUserService currentUserService,
    [FromServices] ISender sender) =>
    {
        var customerId = currentUserService.UserId;
        await sender.Send(new SubmitCart(customerId!.Value));
        return Results.NoContent();
    })
    .WithName("Submit");

cartGroup.MapDelete("/", async (
    [FromServices] ICurrentUserService currentUserService,
    [FromServices] ISender sender) =>
    {
        var customerId = currentUserService.UserId;
        await sender.Send(new DeleteCart(customerId!.Value));
        return Results.NoContent();
    })
    .WithName("Delete");

app.Run();