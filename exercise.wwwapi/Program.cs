using exercise.wwwapi.Controllers.Data;
using exercise.wwwapi.Controllers.Models;
using exercise.wwwapi.Controllers.Models.DTO;
using exercise.wwwapi.Controllers.Repository;
using exercise.wwwapi.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseInMemoryDatabase("Products");
});
builder.Services.AddScoped<IProductRepository<Product>, ProductRepository<Product>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Configure Endpoint:
app.ConfigureProductEndpoint();


//Put data in database:
app.MapGet("/seed", (IProductRepository<Product> productRepository) =>
{
    ProductCollection productsColl = new ProductCollection();
    productsColl.collection.ForEach(product => { productRepository.Add(product); });
});

app.Run();

