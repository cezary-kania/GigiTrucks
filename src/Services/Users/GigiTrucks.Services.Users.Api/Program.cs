using GigiTrucks.Services.Users.Core;
using GigiTrucks.Services.Users.Core.Features.SignIn;
using GigiTrucks.Services.Users.Core.Features.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCore(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello Users!");

app.MapPost("/sign-in", async (
        [FromBody] SignIn command,
        [FromServices] ISender sender) => await sender.Send(command))
    .WithTags("Identity")
    .WithName("Sign In")
    .WithOpenApi();

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