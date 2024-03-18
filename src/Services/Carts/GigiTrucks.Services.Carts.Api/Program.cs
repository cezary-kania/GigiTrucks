using GigiTrucks.Services.Carts.Api.Requests;
using GigiTrucks.Services.Carts.Application;
using GigiTrucks.Services.Carts.Application.Commands.CreateCart;
using GigiTrucks.Services.Carts.Application.Commands.DeleteCart;
using GigiTrucks.Services.Carts.Application.Commands.SubmitCart;
using GigiTrucks.Services.Carts.Application.Commands.UpdateCart;
using GigiTrucks.Services.Carts.Application.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Carts");

var cartGroup = app.MapGroup("api/cart")
    .WithName("Cart");

cartGroup.MapGet("/{cartId:Guid}",
    (Guid cartId) =>
    {
        var mockedResult = new CartDetailsDto
        (
            cartId,
            Guid.NewGuid(),
            new List<CartItemDto>
            {
                new(Guid.NewGuid(), 1)
            }
        );
        return Results.Ok(mockedResult);
    }
).WithName("Get Cart");

cartGroup.MapPost("/", async (
        [FromBody] CreateCartRequest request,
        [FromServices] ISender sender) =>
    {
        var customerId = Guid.NewGuid(); // FIXME: Replace with current user Id
        var createCartCommand = request.Adapt<CreateCart>() with { CustomerId = customerId, CartId = Guid.NewGuid() };
        await sender.Send(createCartCommand);
        return Results.CreatedAtRoute("Get Cart", createCartCommand.CartId);
    }
).WithName("Create Cart");

cartGroup.MapPut("/{cartId:Guid}", async (
        [FromRoute] Guid cartId,
        [FromBody] UpdateCartRequest request,
        [FromServices] ISender sender) =>
    {
        var updateCartCommand = request.Adapt<UpdateCart>() with { CartId = cartId };
        await sender.Send(updateCartCommand);
        return Results.NoContent();
    }
).WithName("Update Cart");

cartGroup.MapPatch("/{cartId:Guid}", async (
        [FromRoute] Guid cartId,
        [FromServices] ISender sender) =>
    {
        await sender.Send(new SubmitCart(cartId));
        return Results.NoContent();
    }
).WithName("Submit");

cartGroup.MapDelete("/{cartId:Guid}", async (
        [FromRoute] Guid cartId,
        [FromServices] ISender sender) =>
    {
        await sender.Send(new DeleteCart(cartId));
        return Results.NoContent();
    }
).WithName("Delete");

app.Run();