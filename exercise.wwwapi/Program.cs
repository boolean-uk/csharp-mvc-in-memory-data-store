using exercise.wwwapi.Data;
using exercise.wwwapi.Endpoints;
using exercise.wwwapi.Repositories.Discounts;
using exercise.wwwapi.Repositories.Producs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ProductContext>(ops =>
{
    ops.UseInMemoryDatabase("ProductsList");
});

builder.Services.AddDbContext<DiscountContext>(ops =>
{
    ops.UseInMemoryDatabase("DiscountList");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureProductEndpoints();
app.ConfigureDiscountEndpoints();

/*
var productsGroup = app.MapGroup("/products");

productsGroup.MapGet("/", (IProductRepository product) =>
{
    return TypedResults.Ok(product.getAllProducts());
});*/


app.Run();

