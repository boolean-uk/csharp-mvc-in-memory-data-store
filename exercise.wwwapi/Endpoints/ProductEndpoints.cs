using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapGet("/",GetAllProducts);
            productGroup.MapGet("/{id}", GetProductById);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAllProducts(IRepository repository, string? category)
        {
            var products = repository.GetAllProducts(category);
            if (products.Count() == 0)
            {
                return TypedResults.NotFound("No products of the provided category were found.");
            }
            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static async Task<IResult> GetProductById(IRepository repository, int id)
        {
            var product = repository.GetProduct(id);

            if(product == null)
            {
                return TypedResults.NotFound("Not found");
            }
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public static async Task<IResult> AddProduct(IRepository repository, PostProduct product)
        {
            if(product.price.GetType() != typeof(int))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided");
            }
            if(repository.GetAllProducts(null).Where(x => x.name == product.name).Count() > 0)
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }
            repository.AddProduct(product);
            return TypedResults.Created("Created new product", product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static async Task<IResult> UpdateProduct(IRepository repository, int id, PutProduct model)
        {
            if(repository.GetProduct(id) == null)
            {
                return TypedResults.NotFound("Product not found");
            }
            if (repository.GetAllProducts(null).Where(x => x.name == model.name).Count() > 0)
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }
            if (model.price.GetType() != typeof(int))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided");
            }
            return TypedResults.Ok(repository.UpdateProduct(id, model));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            var result = repository.DeleteProduct(id);
            if (result == null)
            {
                return TypedResults.NotFound("Product not found");
            }
            return TypedResults.Ok(repository.DeleteProduct(id));
        }
    }
}
