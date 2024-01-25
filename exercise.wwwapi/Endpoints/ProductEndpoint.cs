
using exercise.wwwapi.Models;
using exercise.wwwapi.Models.DTO;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapPost("/", CreateProduct);
            productGroup.MapGet("/", GetAllProucts);
            productGroup.MapGet("/{id}", GetProductById);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateProduct(IProductRepository repository, [FromBody] CreateProductDTO c)
        {
            if (!int.TryParse(c.Price.ToString(), out _))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided.");
            }
            Product result = await repository.CreateProductAsync(c);
            if (result == null)
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }
            return TypedResults.Created($"https://localhost:7280/products/{result.Id}", result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAllProucts(IProductRepository repository, [FromQuery] string? category)
        {
            List<Product> products = await repository.GetAllProductsAsync(category == null ? string.Empty : category);
            if (products.Count == 0)
            {
                return TypedResults.NotFound("No products of the provided category were found");
            }
            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductById(IProductRepository repository, int id)
        {
            Product product = await repository.GetProductByIdAsync(id);
            if (product == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, int id, [FromBody] CreateProductDTO updateDTO)
        {
            if (!int.TryParse(updateDTO.Price.ToString(), out _))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided.");
            }
            Tuple<Product, int> result = await repository.UpdateProductByIdAsync(id, updateDTO);
            if (result.Item1 == null)
            {
                if (result.Item2 == -1)
                {
                    return TypedResults.BadRequest("Product with provided name already exists.");
                }
                else
                {
                    return TypedResults.NotFound("Product not found.");
                }
            }
            return TypedResults.Created($"https://localhost:7280/products/{result.Item1.Id}", result.Item1);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository repository, int id)
        {
            Product product = await repository.DeleteProductByIdAsync(id);
            if (product == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            return TypedResults.Ok(product);
        }
    }
}
