using GigiTrucks.Services.Users.Core;
using GigiTrucks.Services.Users.Core.Features.SignIn;
using GigiTrucks.Services.Users.Core.Features.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCore(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello Users!");

app.MapPost("/sign-up", async (
        [FromBody] SignUp command,
        [FromServices] ISender sender) =>
    {
        await sender.Send(command);
        return Results.NoContent();
    })
    .WithTags("Identity")
    .WithName("Sign Up");

app.Run();