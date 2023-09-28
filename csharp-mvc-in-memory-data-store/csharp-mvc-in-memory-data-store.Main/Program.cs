using mvc_in_memory_data_store.Controllers;
using Microsoft.OpenApi.Models;
using mvc_in_memory_data_store.Data;
using mvc_in_memory_data_store.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Counter API",
        Description = "An API providing ways to change a counter",
        Contact = new OpenApiContact
        {
            Name = "Coder",
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();