using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Data;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("ProductDb"));

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (BadHttpRequestException ex)
    {
        var exceptionMessage = ex.Message;
        await Results.BadRequest($"Price must be an integer, something else was provided").ExecuteAsync(context);
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureProductEndpoint();

app.Run();

