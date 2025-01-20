using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controllers;

public static class ProductController
{
    public static void ConfigureEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products");
        
        group.MapGet("/", GetAllProducts);
        group.MapGet("/{id}", GetProduct);
        group.MapPost("/", CreateProduct);
        group.MapDelete("/{id}", DeleteProduct);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static IResult GetAllProducts([FromServices] IRepository<Product> repository, HttpContext context)
    {
        var products = repository.GetAll();
        // TODO: Do a check and return 404 if empty
        return TypedResults.Ok(products);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    private static void GetProduct()
    {
        // Implement this
        throw new NotImplementedException();
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    private static void CreateProduct()
    {
        // Implement this
        throw new NotImplementedException();
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static void DeleteProduct()
    {
        // Implement this
        throw new NotImplementedException();
    }
}