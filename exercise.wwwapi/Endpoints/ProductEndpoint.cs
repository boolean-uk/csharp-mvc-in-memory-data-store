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

            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPost("/", CreateProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllProducts(IProductRepository repository)
        {
            var products = repository.GetAllProducts();
            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IProductRepository repository, int id)
        {
            var product = repository.GetProductById(id);

            if (product == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateProduct(IProductRepository repository, [FromBody] ProductPost model)
        {
            if (model == null)
            {
                return TypedResults.BadRequest("Invalid data provided.");
            }

            var newProduct = new Product
            {
                Name = model.Name,
                Category = model.Category,
                Price = model.Price
            };

            repository.CreateProduct(newProduct);

            return TypedResults.Created($"/{newProduct.Id}", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, int id, [FromBody] ProductPut model)
        {
            if (model == null)
            {
                return TypedResults.BadRequest("Invalid data provided.");
            }

            var updatedProduct = repository.UpdateProduct(id, model);

            if (updatedProduct == null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(updatedProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository repository, int id)
        {
            repository.DeleteProduct(id);
            return TypedResults.NoContent();
        }
    }
}
