using exercise.wwwapi.Data;
using exercise.wwwapi.Endpoints;
using exercise.wwwapi.Repository;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository, ProductRepository>();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("products"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.ConfigureProductEndpoints();

app.Run();

