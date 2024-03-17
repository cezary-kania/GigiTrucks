using GigiTrucks.Services.Orders.Application;
using GigiTrucks.Services.Orders.Application.Commands.ApproveOrder;
using GigiTrucks.Services.Orders.Infrastructure;
using GigiTrucks.Services.Orders.Infrastructure.Queries.GetOrder;
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

app.MapGet("/", () => "Hello Orders!");

var ordersGroup = app.MapGroup("api/order")
    .WithTags("Orders");

ordersGroup.MapGet("/{orderId:Guid}", async (
        [FromRoute]Guid orderId,
        [FromServices] ISender sender) =>
    {
        var orderDetails = await sender.Send(new GetOrder(orderId));
        return Results.Ok(orderDetails);
    })
    .WithName("Get Order")
    .WithOpenApi();

ordersGroup.MapPut("/{orderId:Guid}/approve", async (
        [FromRoute]Guid orderId,
        [FromServices] ISender sender) 
        => await sender.Send(new ApproveOrder(orderId)))
    .WithName("Approve Order")
    .WithOpenApi();

app.Run();