using GigiTrucks.Services.Carts.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Carts");

var cartGroup = app.MapGroup("api/cart")
    .WithName("Cart")
    .RequireAuthorization();

cartGroup.MapGet("/{cartId:Guid}",
    (Guid cartId) =>
    {
        var mockedResult = new CartDetailsDto
        (
            cartId,
            Guid.NewGuid(),
            new List<CartItemDto>
            {
                new(Guid.NewGuid())
            }
        );
        return Results.Ok(mockedResult);
    }
);

app.Run();