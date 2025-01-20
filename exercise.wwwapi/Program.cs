using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using exercise.wwwapi.Data;
using exercise.wwwapi.Repositories;
using exercise.wwwapi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Products"));
//builder.Services.AddDbContext<DataContext>(options => options.UseInMemory ("Products"));

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

