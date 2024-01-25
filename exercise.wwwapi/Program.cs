
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using wwwapi.Data;
using wwwapi.EndPoints;
using wwwapi.Models;
using wwwapi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductContext>(opt =>
{
    opt.UseInMemoryDatabase("ProductList");
});

builder.Services.AddScoped<IRepository<Product>, ProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.ConfigureProductEndPoints();

app.Run();

