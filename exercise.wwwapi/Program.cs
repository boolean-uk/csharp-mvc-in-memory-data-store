using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using exercise.wwwapi.Data;
using exercise.wwwapi.Endpoints;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductsContext>(opt =>
{
    opt.UseInMemoryDatabase("ProductList");
});

builder.Services.AddScoped<ProductRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



var productGroup = app.MapGroup("products");
productGroup.MapPost("/", ProductEndpoints.CreateProduct);
productGroup.MapGet("/{id}", ProductEndpoints.GetProduct);
productGroup.MapGet("/", ProductEndpoints.GetAllProducts);
productGroup.MapPut("/{id}", ProductEndpoints.UpdateProduct);
productGroup.MapDelete("/{id}", ProductEndpoints.DeleteProduct);

      



app.Run();

