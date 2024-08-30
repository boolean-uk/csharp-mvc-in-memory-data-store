using exercise.wwwapi.Models.Data;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controller
{
    [ApiController]
    [Route("products")]
    public static class ProductController
    {
        public static void ConfigureProductController(this WebApplication app)
        {
            var products = app.MapGroup("/");
            products.MapGet("/", GetAll);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", AddProduct);
            products.MapPut("/", UpdateProduct);
            products.MapDelete("/", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IProductRepository repository, int id)
        {
            var result = repository.DeleteProduct(id);
            return result == null ? TypedResults.NotFound(result) : TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IProductRepository repository, int id, Product product)
        {
            var result = repository.UpdateProduct(id, product);
            return result == null ? TypedResults.NotFound(result) : TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult AddProduct(IProductRepository repository, Product product)
        {
            var result = repository.AddProduct(product);
            return result == null ? TypedResults.BadRequest(result) : TypedResults.Created();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IProductRepository repository, int id)
        {
            var result = repository.GetProduct(id);
            return result == null ? TypedResults.NotFound(result) : TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAll(IProductRepository repository)
        {
            var result = repository.GetAll();
            return result == null ? TypedResults.NotFound(result) : TypedResults.Ok(result);
        }
    }
}
