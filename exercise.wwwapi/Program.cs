using Microsoft.EntityFrameworkCore;
using exercise.wwwapi.Data;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Models;
using exercise.wwwapi.Endpoints;
using NuGet.Packaging.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductContext>(opt => opt.UseInMemoryDatabase("ProductDb"));
builder.Services.AddScoped<IRepository, Repository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (BadHttpRequestException ex)
    {
        var exceptionMessage = ex.Message;
        if (ex == null) { TypedResults.BadRequest("Price must be an integer, something else was provided.").ExecuteAsync(context); }
        await Results.BadRequest($"{exceptionMessage}").ExecuteAsync(context);

    }
});

app.UseHttpsRedirection();
app.ConfigureEndpoint();

app.Run();

