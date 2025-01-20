using genericapi.api.Data;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using genericapi.api.Repository;
using genericapi.api.Models;
using exercise.wwwapi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository<Product, Guid>, ProductRepository>();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Products"));


var app = builder.Build();

var options = new DbContextOptionsBuilder<DataContext>()
    .UseInMemoryDatabase("Products")
    .Options;

using (var context = new DataContext(options))
{
    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product { Id = new Guid("0483b59c-f7f8-4b21-b1df-5149fb57984e"), Name = "Fanta", Category = "Soft drink", Price = 12 },
            new Product { Id = new Guid("0483b59c-f7f8-4b21-b2df-5149fb57984e"), Name = "Cola", Category = "Soft drink", Price = 13 },
            new Product { Id = new Guid("0483b59c-f7f8-4b21-b3df-5149fb57984e"), Name = "Bread", Category = "Baked goods", Price = 3 }
        );
        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.ConfigureProductEndpoint();

app.Run();

