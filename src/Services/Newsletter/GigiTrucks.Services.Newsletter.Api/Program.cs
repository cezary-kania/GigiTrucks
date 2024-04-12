using GigiTrucks.Services.Common.Identity;
using GigiTrucks.Services.Newsletter.Application.Commands.Subscribe;
using GigiTrucks.Services.Newsletter.Application.Commands.Unsubscribe;
using GigiTrucks.Services.Newsletter.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Newsletter");

app.MapPost("/subscribe", async (
        [FromServices] ICurrentUserService currentUserService,
        [FromServices] ISender sender) =>
    {
        var subscriberId = currentUserService.UserId;
        var subscriberEmail = currentUserService.UserEmail;
        if (subscriberId is null || subscriberEmail is null)
        {
            return Results.Unauthorized();
        }
        await sender.Send(new Subscribe(subscriberId.Value, subscriberEmail));
        return Results.NoContent();
    })
    .WithName("Subscribe")
    .WithOpenApi();

app.MapDelete("/unsubscribe", async (
        [FromServices] ICurrentUserService currentUserService,
        [FromServices] ISender sender) => 
    {
            var subscriberId = currentUserService.UserId;
            if (subscriberId is null)
            {
                return Results.Unauthorized();
            }
            await sender.Send(new Unsubscribe(subscriberId.Value));
            return Results.NoContent();
    })
    .WithName("Unsubscribe")
    .WithOpenApi();

app.Run();