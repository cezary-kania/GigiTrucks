using GigiTrucks.Services.Common.Identity;
using GigiTrucks.Services.Discounts.Application.Queries.GetUserDiscounts;
using GigiTrucks.Services.Discounts.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddCurrentUserService()
    .AddHttpContextAccessor()
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var discountGroup = app.MapGroup("api/discount")
    .WithTags("Discount");

discountGroup.MapGet("/user-discounts", async (
    [FromServices] ICurrentUserService currentUserService,
    [FromServices] ISender sender) => 
    {
        var userId = currentUserService.UserId;
        if (userId is null)
        {
            return Results.Unauthorized();
        }
        await sender.Send(new GetUserDiscountsQuery(userId.Value));
        return Results.NoContent();
    })
    .WithName("GetUserDiscounts");

app.Run();