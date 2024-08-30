

using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapGet("/", GetProducts);
            products.MapPost("/", CreateProduct);
            products.MapGet("/{name}", GetProduct);
            products.MapPut("/{name}", UpdateProduct);
            products.MapDelete("/", DeleteProduct);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository repository, int id)
        {
            if (!repository.ContainsProduct(id))
            {
                // product does not exist
                return TypedResults.NotFound("Product not found!");
            }
            var result = repository.DeleteProduct(id);
            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IRepository repository, Payload payload, int id)
        {
            if(repository.ContainsProduct(payload.name))
            {
                // existing product does  exist
                return TypedResults.BadRequest("Name already exists!");
            }
            else if(!repository.ContainsProduct(id))
            {
                // product does not exist
                return TypedResults.NotFound("Product not found!");
            }
            var result = repository.UpdateProduct(payload, id);
            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IRepository repository, int id)
        {
            if (repository.ContainsProduct(id)) //item exists
            {
                var result = repository.GetProduct(id);
                return TypedResults.Ok(result);
            }
            //does not exist
            return TypedResults.NotFound("Product not found!");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IRepository repository, Payload payload)
        {
            if(repository.ContainsProduct(payload.name)) //already exists
            {
                return TypedResults.BadRequest("Name already exists!");
            }
            var result = repository.CreateProduct(payload);
            return TypedResults.Created($"https://localhost:7188/products/{result.id}", result);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProducts(IRepository repository, string category)
        {
            var result = repository.GetProducts(category);
            if(result.Count == 0)
            {
                return TypedResults.NotFound("No products in the provided category found!");
            }
            return TypedResults.Ok(result);
        }
    }
}
