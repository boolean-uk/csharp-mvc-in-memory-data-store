using exercise.wwwapi.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

using exercise.wwwapi.Endpoints;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Products"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Demo API");
    });
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.ProductEndpointsConfigure();

app.Run();

