using exercise.wwwapi.Controllers;
using exercise.wwwapi.DB;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddDbContext<ProductContext>(o => o.UseInMemoryDatabase("bagelDB"));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// TODO: Remove! This is just for testing
var product = new Product
{
    Name = "Bagel",
    Price = 1.99m,
    Category = "Food"
};

using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetRequiredService<IRepository<Product>>();
    repository.Create(product);
}

// TODO: Keep the remaining code

app.UseHttpsRedirection();

app.ConfigureEndpoints();

app.Run();

