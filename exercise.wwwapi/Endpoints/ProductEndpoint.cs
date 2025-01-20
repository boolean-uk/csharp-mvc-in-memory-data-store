using System;
using EFCore.Repository.Contracts.Repository;
using exercise.wwwapi.Models;
using exercise.wwwapi.ModelViews;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticAssets;

namespace exercise.wwwapi.Endpoints;

public static class ProductEndpoint
{
    public static void ConfigureProductEndpoint(this WebApplication app)
    {
        app.MapGroup("products");

        app.MapGet("/", GetAllProducts);
        app.MapGet("/{id}", GetProduct);
        app.MapPost("/", AddProduct);
        app.MapPut("/{id}", UpdateProduct);
        app.MapDelete("/{id}", DeleteProduct);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    public static async Task<IResult> GetAllProducts(IProductRepository repo, string category = "")
    {
        Task<IEnumerable<ProductResponse>> result = repo.GetProducts(category);
        if (result.Result.ToList()[0].Status == "Not Found")
        {
            return TypedResults.NotFound($"No products of the category {category} were found");
        }
        return TypedResults.Ok(result.Result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> GetProduct(IProductRepository repo, int id)
    {
        Task<ProductResponse> result = repo.GetProduct(id);

        if (result.Result.Status == "Not Found")
        {
            return TypedResults.NotFound("Product not found");
        }

        return TypedResults.Ok(result.Result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public static async Task<IResult> AddProduct(IProductRepository repo, ProductPost prodPost)
    {
        Task<ProductResponse> result = repo.CreateProduct(prodPost);

        if (result.Result.Status == "Conflict")
        {
            return TypedResults.BadRequest($"Product name already exists: {prodPost.Name}");
        }
        
        return TypedResults.Ok(result.Result);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static async Task<IResult> UpdateProduct(IProductRepository repo, int id, ProductPut prodPut)
    {
        Task<ProductResponse> result = repo.UpdateProduct(id, prodPut);

        if (result.Result.Status == "Not Found")
        {
            return TypedResults.NotFound($"Product with id = {id} does not exist!");
        }

        else if (result.Result.Status == "Conflict")
        {
            return TypedResults.BadRequest($"Product name already exist: {prodPut.Name}");
        }


        return TypedResults.Ok(result.Result);
    }


    public static async Task<IResult> DeleteProduct(IProductRepository repo, int id)
    {
        Task<ProductResponse> result = repo.DeleteProduct(id);

        if (result.Result.Status == "Not Found")
        {
            return TypedResults.NotFound($"Product with id = {id} does not exist!");
        }

        return TypedResults.Ok(result.Result);
    }
}
