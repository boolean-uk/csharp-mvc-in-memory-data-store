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
        Title = "C# API Exercise - Extension Criteria",
        Description = "C# API MVC in-memory List Extension Criteria",
        Contact = new OpenApiContact
        {
            Name = "MVC in Memory Extension Exercise",
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
