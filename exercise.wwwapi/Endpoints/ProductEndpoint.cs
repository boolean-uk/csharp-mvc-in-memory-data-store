using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints;

public static class ProductEndpoint
{
    public static void ConfigureProductEndpoint(this WebApplication app)
    {
        var productGroup = app.MapGroup("products");

        productGroup.MapPost("/create/", AddProduct);
        productGroup.MapGet("/", GetProducts);
        productGroup.MapGet("/{id}", GetProduct);
        productGroup.MapPut("/{id}", UpdateProduct);
        productGroup.MapDelete("/{id}", DeleteProduct);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    public static async Task<IResult> AddProduct(IRepository repository, PostProduct product)
    {
        if (!int.TryParse(product.Price, out _))
        {
            return TypedResults.BadRequest("Price must be an int!");
        }
        if (repository.ProductExists(product.Name, out _))
        {
            return TypedResults.BadRequest($"Product with the name: {product.Name} already exists!");
        }

        var newProduct = repository.AddProduct(product);
        return TypedResults.Created($"/{newProduct.Id}", newProduct);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> GetProducts(IRepository repository, string? category)
    {
        if (category != null)
        {
            var catProducts = repository.GetProducts(category);
            if (catProducts.Count() == 0)
            {
                return TypedResults.NotFound($"Products of category: {category} not found!");
            }

            return TypedResults.Ok(catProducts);
        }

        return TypedResults.Ok(repository.GetProducts());
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> GetProduct(IRepository repository, int id)
    {
        var product = repository.GetProduct(id);

        if (product == null)
        {
            return TypedResults.NotFound($"Id: {id} not found!");
        }

        return TypedResults.Ok(product);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> UpdateProduct(IRepository repository, int id, PutProduct product)
    {
        int existingId = 0;
        if (repository.ProductExists(product.Name, out existingId))
        {
            if (existingId != id)
            {
                return TypedResults.BadRequest($"Product with the name: {product.Name} already exists!");
            }
        }

        var edited = repository.UpdateProduct(id, product);

        if (edited == null)
        {
            return TypedResults.NotFound($"Id: {id} not found!");
        }

        return TypedResults.Ok(edited);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> DeleteProduct(IRepository repository, int id)
    {
        var removed = repository.DeleteProduct(id);
        if (removed == null)
        {
            return TypedResults.NotFound($"Id: {id} not found!");
        }
        return TypedResults.Ok(removed);
    }
}
