using exercise.wwwapi.Data;
using exercise.wwwapi.Endpoints;
using exercise.wwwapi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ErrorCollection>();

builder.Services.AddDbContext<ProductContext>(opt =>
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


//Products
app.MapPost("/products", ProductEndpoints.AddProduct);
app.MapGet("/products", ProductEndpoints.GetAllProducts);
app.MapGet("/products{id}", ProductEndpoints.GetProduct);
app.MapPut("/products{id}", ProductEndpoints.UpdateProduct);
app.MapDelete("/products{id}", ProductEndpoints.DeleteProduct);



app.Run();

