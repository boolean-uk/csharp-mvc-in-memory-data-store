using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints;

public static class ProductEndpoint
{
    public static void ConfigureProductEndpoint(this WebApplication app)
    {
        var prod = app.MapGroup("prod");
        prod.MapGet("/", GetAllProducts);
        prod.MapPost("/", AddProduct);
        prod.MapGet("/{id}", GetProduct);
        prod.MapPut("/{id}", UpdateProduct);
        prod.MapDelete("/{id}", DeleteProduct);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static IResult GetAllProducts(IRepository<Product> repo, string category)
    {
        var l = repo.GetAll(category);
        return l.Count > 0 ? TypedResults.Ok(l) : TypedResults.NotFound();
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public static IResult AddProduct(IRepository<Product> repo, PutProduct input)
    {
        try
        {
            var pp = input.ToProduct();
            if (repo.GetAll(pp.Category).Any(x => x.Name.Equals(pp.Name, StringComparison.CurrentCultureIgnoreCase))) return TypedResults.BadRequest("Product already exists.");
            var p = repo.Add(new Product(pp.Name, pp.Category, pp.Price));
            return TypedResults.Created($"/{p.Id}", p);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest("Price must be an integer.");
        }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static IResult GetProduct(IRepository<Product> repo, int id)
    {
        var p = repo.Get(id);
        if (p is null) return TypedResults.NotFound();
        return TypedResults.Ok(p);
    }
    
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static IResult UpdateProduct(IRepository<Product> repo, int id, PutProduct input)
    {
        try
        {
            var pp = input.ToProduct();
            var p = repo.Get(id);
            if (p is null) return TypedResults.NotFound();
            if (repo.GetAll(pp.Category).Any(x => x.Name.Equals(pp.Name, StringComparison.CurrentCultureIgnoreCase))) return TypedResults.BadRequest("Product already exists.");
            return TypedResults.Created($"/{p.Id}", repo.Update(p, pp));
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest("Price must be an integer.");
        }
    }
    
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static IResult DeleteProduct(IRepository<Product> repo, int id)
    {
        var p = repo.Get(id);
        if (p is null) return TypedResults.NotFound();
        return TypedResults.Ok(repo.Delete(p));
    }
}