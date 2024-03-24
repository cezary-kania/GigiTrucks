using GigiTrucks.Services.Products.Core;

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

app.UseHttpsRedirection();

app.MapGet("/", () => "Products");

app.Run();