using System;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repositories;
using exercise.wwwapi.View;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace exercise.wwwapi.Endpoints;

public static class ProductEndpoint
{
    public static void ConfigureProductEndpoint(this WebApplication app) 
    {
        var products = app.MapGroup("products");

        products.MapPost("/", AddProduct);
        products.MapGet("/", GetProducts);
        products.MapGet("/{id}", GetProduct);
        products.MapPut("/{id}", UpdateProduct);
        products.MapDelete("/{id}", DeleteProduct);
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public static async Task<IResult> AddProduct(ProductPost productView, IRepository repository)
    {
        try
            {

            Product Product = new Product()
            {
                Id = new Guid(),
                Name = productView.Name,
                Price = productView.Price,
                Category = productView.Category
            };
            await repository.AddProduct(Product);

            return TypedResults.Created($"{Product.Id}", Product);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> GetProducts(IRepository repository)
    {
        var products = await repository.GetProducts();
        if (products.Count() == 0)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(products);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> GetProduct(Guid id, IRepository repository)
    {
        try
        {
            var product = await repository.GetProduct(id);
            return TypedResults.Ok(product);
        }
        catch (KeyNotFoundException ex) 
        { 
            return TypedResults.NotFound(ex.Message);
        }
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> UpdateProduct(Guid id, ProductPut productView, IRepository repository)
    {
        try
        {
            var product = await repository.UpdateProduct(id, productView);
            return TypedResults.Created($"Updated {product.Id}", product);
        }
        catch (KeyNotFoundException ex)
        {
            return TypedResults.NotFound(ex.Message);
        }        
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> DeleteProduct(Guid id, IRepository repository)
    {
        var deleted = await repository.DeleteProduct(id);
        
        if (deleted)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.NotFound();
        }
    }
}
