using exercise.wwwapi.Data;
using exercise.wwwapi.EndPoint;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<InternalProduct>, Repository<InternalProduct>>();
builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();
builder.Services.AddDbContext<ProductContex>(opt => opt.UseInMemoryDatabase("ProductDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureProductEndpoint();

app.Run();

