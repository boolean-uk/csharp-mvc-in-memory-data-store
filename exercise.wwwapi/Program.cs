using exercise.wwwapi.Endpoints;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Validators;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using exercise.wwwapi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Products"));
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ProductPostValidator));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureProductEndpoints();

app.Run();

