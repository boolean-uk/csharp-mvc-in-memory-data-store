using exercise.wwwapi.Repository;
using exercise.wwwapi.Data;
using exercise.wwwapi.Products;
using exercise.wwwapi.Endpoints;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddSingleton<IProduct, ProductCollections>();  
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureProductEndpoint();

app.Run();

