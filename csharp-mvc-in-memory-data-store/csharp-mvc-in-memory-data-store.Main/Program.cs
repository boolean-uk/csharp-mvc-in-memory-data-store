using mvc_in_memory_data_store.Controllers;
using Microsoft.OpenApi.Models;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IBagelRepository, BagelRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // added for Products
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API exercise products",
        Description = "An API for product crud operations",
        Contact = new OpenApiContact
        {
            Name = "Coder",
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
