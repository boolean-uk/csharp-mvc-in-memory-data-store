using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints;

public static class ProductEndpoints
{
    public static void ConfigureEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/products");
        
        group.MapGet("/", GetAllProducts);
        group.MapGet("/{id}", GetProduct);
        group.MapPost("/", CreateProduct);
        group.MapPut("/{id}", UpdateProduct);
        group.MapDelete("/{id}", DeleteProduct);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static IResult GetAllProducts([FromServices] IRepository<Product> repository, HttpContext context, string? category)
    {
        List<Product> products;
        
        products = category is not null ? repository.GetAll(category) : repository.GetAll();
        
        if (products.Count == 0)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(products);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static IResult GetProduct([FromServices] IRepository<Product> repository, Guid id)
    {
        Product? product = repository.Read(id);
        
        if (product is null)
        {
            return TypedResults.NotFound();
        }
        
        return TypedResults.Ok(product);
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    private static IResult CreateProduct([FromServices] IRepository<Product> repository, Product product)
    {
        // Checks if product already exists. Duplicate name is bad
        if (repository.GetByName(product.Name) is not null)
        {
            return TypedResults.Conflict();
        }
        
        Product createdProduct = repository.Create(product);
        
        return TypedResults.Created(createdProduct.Id.ToString());
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static IResult UpdateProduct([FromServices] IRepository<Product> repository, Guid id, Product product)
    {
        var existingProduct = repository.Read(id);
        if (existingProduct is null)
        {
            return TypedResults.NotFound();
        }

        // I get System.InvalidOperationException without this :(
        repository.Detach(existingProduct);
        
        product.Id = id;
        repository.Update(product);
        
        return TypedResults.NoContent();
    }
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    private static IResult DeleteProduct([FromServices] IRepository<Product> repository, Guid id)
    {
        if (repository.Read(id) is null)
        {
            return TypedResults.NotFound();
        }
        
        repository.Delete(id);
        
        return TypedResults.NoContent();
    }
}