using GigiTrucks.Services.Users.Api.Exceptions;
using GigiTrucks.Services.Users.Core;
using GigiTrucks.Services.Users.Core.Features.SignIn;
using GigiTrucks.Services.Users.Core.Features.SignUp;
using GigiTrucks.Services.Users.Core.Health;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCore(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddHealthChecks()
    .AddInfrastructureHealthChecks(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();
app.UseSerilogRequestLogging();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponseNoExceptionDetails
});

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

public partial class Program;