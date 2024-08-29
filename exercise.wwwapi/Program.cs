using exercise.wwwapi.Data;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Services;
using exercise.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register an in memory DbContext
builder.Services.AddDbContext<ProductDbContext>(options => options.UseInMemoryDatabase("ProductDB"));
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IRepository<Product>, Repository<Product>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

