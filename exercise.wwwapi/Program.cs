using exercise.wwwapi.Data;
using exercise.wwwapi.EndPoints;
using exercise.wwwapi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("ProductDb"));
builder.Services.AddScoped<IRepository, ProductRepository>();   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*Exception handling*/
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

app.ConfigureProductEndpoints();

app.Run();

