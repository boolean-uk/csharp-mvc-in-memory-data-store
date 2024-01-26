using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository, string? category)
        {
            if (category == null) return TypedResults.Ok(repository.GetAll());

            var result = repository.GetAll(category);
            if (result.Any()) return TypedResults.Ok(result);

            return Results.NotFound("No products of the provided category were found.");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductParameters product)
        {
            if (!product.Name.Any() || !product.Category.Any())
            {
                return Results.BadRequest("Missing some data.");
            }
            if (repository.GetAll().Any(p => p.Name == product.Name))
            {
                return Results.BadRequest("Product with provided name already exists.");
            }
            Product newProduct = repository.Add(product);
            return TypedResults.Created($"/{newProduct.Id}", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            var product = repository.GetAll().FirstOrDefault(p => p.Id == id);

            if (product != null) return TypedResults.Ok(product);
            return Results.NotFound($"Product not found.");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductParameters newProduct)
        {
            var product = repository.GetAll().First(s => s.Id == id);

            if (product == null) return Results.NotFound($"Product not found.");
            if (!product.Name.Any() || !product.Category.Any())
            {
                return Results.BadRequest("Missing some data");
            }
            if (repository.GetAll().Any(p => p.Name == newProduct.Name))
            {
                return Results.BadRequest("Product with provided name already exists.");
            }

            Product updatedProduct = repository.Update(product, newProduct);
            return TypedResults.Created($"/{updatedProduct.Id}", updatedProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            var product = repository.GetAll().First(p => p.Id == id);

            if (product == null) return Results.NotFound($"Product not found.");

            repository.Delete(product);
            return TypedResults.Ok(product);

        }
    }
}
