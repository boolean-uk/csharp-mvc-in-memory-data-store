namespace exercise.wwwapi.endpoint;

using exercise.wwwapi.model;
using exercise.wwwapi.repository;
using exercise.wwwapi.viewmodel;
using Microsoft.AspNetCore.Mvc;

public static class ProductEndpoint
{
    public static void ConfigureProductEndpoint(this WebApplication app)
    {
        var products = app.MapGroup("pets");

        products.MapGet("/", GetAll);
        products.MapGet("/{id}", Get);
        products.MapPost("/", Add);
        products.MapDelete("/{id}", Delete);
        products.MapPut("/{id}", Update);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> GetAll(
        IRepository<Product, ProductPut> repo,
        string category = ""
    )
    {
        if (category != "")
        {
            return await GetCategory(repo, category);
        }
        var result = await repo.GetAll();
        return TypedResults.Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> Get(IRepository<Product, ProductPut> repo, int id)
    {
        var result = await repo.Get(id);
        if (result == null)
        {
            return TypedResults.NotFound("Product not found");
        }
        return TypedResults.Ok(result);
    }

    private static async Task<IResult> GetCategory(
        IRepository<Product, ProductPut> repo,
        string category
    )
    {
        var products = await repo.GetSome(p => p.Category == category);
        if (products == null)
        {
            return TypedResults.NotFound("No products of the provided category were found");
        }
        return TypedResults.Ok(products);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public static async Task<IResult> Add(IRepository<Product, ProductPut> repo, ProductPost entity)
    {
        var all = await repo.GetAll();
        if (all.Any(p => p.Name == entity.Name))
        {
            return TypedResults.BadRequest("Product with provided name already exists");
        }

        try
        {
            var product = new Product
            {
                Category = entity.Category,
                Name = entity.Name,
                Price = entity.Price,
            };
            var result = await repo.Add(product);
            return TypedResults.Ok(result);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> Delete(IRepository<Product, ProductPut> repo, int id)
    {
        var deletedProduct = await repo.Delete(id);
        if (deletedProduct == null)
        {
            return TypedResults.NotFound("products not found");
        }
        return TypedResults.Ok(deletedProduct);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> Update(
        IRepository<Product, ProductPut> repo,
        int id,
        ProductPut entity
    )
    {
        var all = await repo.GetAll();
        if (all.Any(p => p.Name == entity.Name))
        {
            return TypedResults.BadRequest("Product with provided name already exists");
        }
        Product? result;
        try
        {
            result = await repo.Update(id, entity);
        }
        catch (Exception e)
        {
            return TypedResults.BadRequest(e.Message);
        }
        if (result == null)
        {
            return TypedResults.NotFound("Product not found.");
        }
        return TypedResults.Ok(result);
    }
}
