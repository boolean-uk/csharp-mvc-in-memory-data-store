using exercise.wwwapi.Data;
using exercise.wwwapi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContexts for Product and Discount
builder.Services.AddDbContext<ProductContext>(options => options.UseInMemoryDatabase("ProductDB"));
builder.Services.AddDbContext<DiscountContext>(options => options.UseInMemoryDatabase("DiscountDB"));

// Add repositories for Product and Discount
builder.Services.AddScoped<IProductRepository , ProductRepository>();
builder.Services.AddScoped<IDiscountRepository , DiscountRepository>();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
