using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Data;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Endpoints;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApi();
builder.Services.AddScoped<IRepository, ProductRepository>();
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

app.ConfigureProductEndpoint();

app.Run();

