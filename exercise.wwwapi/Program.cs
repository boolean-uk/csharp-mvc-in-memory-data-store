using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Endpoints;
using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSingleton<ProductCollection>();
builder.Services.AddScoped<IproductRepository, ProductRepository>();

builder.Services.AddDbContext<ProductContext>( opt =>
{
    opt.UseInMemoryDatabase("ProductList");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.configureProductEndpoints();


//Create Product
/*
app.MapPost("/books", (string name, string category, int price, ProductCollection products) =>
{
    Product product = products.AddProduct(name, category, price);
    return TypedResults.Created("/books", product);
});
*/

app.Run();

