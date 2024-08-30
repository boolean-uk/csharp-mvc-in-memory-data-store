

using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapPost("/", CreateProduct);
            products.MapGet("/", GetAllProducts);
            products.MapGet("/{id}", GetAProduct);
            products.MapPut("/{id}", UpdateAProduct);
            products.MapDelete("/{id}", DeleteAProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IRepository<Product, ProductPostModel> repository, ProductPostModel productModel)
        {
            if (repository.Exists(productModel))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }
            var result = repository.Create(productModel);
            return result != null ? TypedResults.Created($"http://localhost:5057/products/{result.id}", result) : TypedResults.BadRequest("Bad Request.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IRepository<Product, ProductPostModel> repository, string category)
        {
            var result = repository.GetAll(category);
            return result.Count != 0 ? TypedResults.Ok(result) : TypedResults.NotFound("Not Found.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAProduct(IRepository<Product, ProductPostModel> repository, int id)
        {
            var result = repository.Get(id);
            return result != null ? TypedResults.Ok(result) : TypedResults.NotFound("Not Found.");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateAProduct(IRepository<Product, ProductPostModel> repository, ProductPostModel productModel, int id)
        {
            if(repository.Exists(productModel))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }
            var result = repository.Update(productModel, id);
            return result != null ? TypedResults.Created($"http://localhost:5057/products/{result.id}", result) : TypedResults.NotFound("Not Found.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteAProduct(IRepository<Product, ProductPostModel> repository, int id)
        {
            var result = repository.Delete(id);
            return result != null ? TypedResults.Ok(result) : TypedResults.NotFound("Not Found.");
        }

    }
}
