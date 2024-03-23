using GigiTrucks.Services.Orders.Application;
using GigiTrucks.Services.Orders.Application.Commands.ApproveOrder;
using GigiTrucks.Services.Orders.Application.Commands.CancelOrder;
using GigiTrucks.Services.Orders.Infrastructure;
using GigiTrucks.Services.Orders.Infrastructure.Health;
using GigiTrucks.Services.Orders.Infrastructure.Queries.GetOrder;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddInfrastructureHealthChecks(builder.Configuration);

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

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponseNoExceptionDetails
});

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

ordersGroup.MapPut("/{orderId:Guid}/cancel", async (
        [FromRoute]Guid orderId,
        [FromServices] ISender sender) 
        => await sender.Send(new CancelOrder(orderId)))
    .WithName("Cancel Order")
    .WithOpenApi();

app.Run();