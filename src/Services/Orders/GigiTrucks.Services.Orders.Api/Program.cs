using GigiTrucks.Services.Orders.Application;
using GigiTrucks.Services.Orders.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddHttpContextAccessor()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
var app = builder.Build();

app.MapGet("/", () => "Hello Orders!");

app.Run();