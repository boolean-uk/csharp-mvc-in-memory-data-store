using exercise.wwwapi.Repository;
using exercise.wwwapi.Models;
using exercise.wwwapi.Data;
using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Endpoint;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("ProductDb"));
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

