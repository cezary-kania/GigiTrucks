using GigiTrucks.Services.Common.Identity;
using GigiTrucks.Services.Newsletter.Application;
using GigiTrucks.Services.Newsletter.Application.Commands.Subscribe;
using GigiTrucks.Services.Newsletter.Application.Commands.Unsubscribe;
using GigiTrucks.Services.Newsletter.Application.Queries.GetSubscriptionStatus;
using GigiTrucks.Services.Newsletter.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
    
builder.Services
    .AddHttpContextAccessor()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

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
    .WithTags("Subscription")
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
    .WithTags("Subscription")
    .WithOpenApi();

app.MapGet("/subscription", async (
        [FromServices] ICurrentUserService currentUserService,
        [FromServices] ISender sender) => 
    {
        var subscriberId = currentUserService.UserId;
        if (subscriberId is null)
        {
            return Results.Unauthorized();
        }

        var subscriptionStatus = await sender.Send(new GetSubscriptionStatus(subscriberId.Value));
        return Results.Ok(subscriptionStatus);
    })
    .WithName("GetSubscriptionStatus")
    .WithTags("Subscription")
    .WithOpenApi();

app.Run();