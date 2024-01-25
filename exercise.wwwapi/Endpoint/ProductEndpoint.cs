using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoint
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("product");

            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetAProduct);
            productGroup.MapPost("/", CreateProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllProducts(IRepository repository, string? category)
        {
            var result = repository.GetAllProducts(category);
            return result.Count() == 0 ? TypedResults.NotFound("No products of the products category were found.") : TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAProduct(IRepository repository, int id)
        {
            Product test = repository.GetAProduct(id);
            if (test == null)
            {
                return Results.NotFound($"Product not found.");
            }
            return TypedResults.Ok(test);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> CreateProduct(IRepository repository, InPuProduct product)
        {
            if (!int.TryParse(product.Price, out int value))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided.");
            }
            var test = repository.AddProduct(product);
            return test == null ? Results.BadRequest("Product with provided name already exists.") : TypedResults.Created($"{product.Name}", product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, InPuProduct product)
        {
            if (!int.TryParse(product.Price, out int value))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided.");
            }

            if (repository.GetAllProducts("").Where(p => p.Id != id).Where(p => p.Name == product.Name).Any())
            {
                return TypedResults.BadRequest("Product with provided name alredy exists.");
            }
            var test = repository.UpdateAProduct(id, product);
            if (test == null)
            {
                return Results.NotFound($"Product not found.");
            }
            return TypedResults.Created($"{product.Name}", product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            Product test = repository.GetAProduct(id);
            return test == null ? Results.NotFound($"Product not found.") : TypedResults.Ok(repository.DeleteABook(id));
        }
    }
}
