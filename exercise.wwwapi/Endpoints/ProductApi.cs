using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductApi
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("/products");
            productGroup.MapGet("", GetAllProducts);
            productGroup.MapGet("/{id}", GetProductById);
            productGroup.MapPost("", CreateProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllProducts(IProductRepository products)
        {
            var results = await products.GetAllAsync();
            return TypedResults.Ok(results);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductById(IProductRepository products, int id)
        {
            var result = await products.GetByIdAsync(id);
            if (result == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found");
            }
            return TypedResults.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateProduct(IProductRepository products, ProductCreatePayload payload)
        {
            var product = await products.AddAsync(payload);
            return TypedResults.Created($"/products/{product.Id}", product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IProductRepository products, int id, ProductUpdatePayload payload)
        {
            Product? product = await products.UpdateAsync(id, payload);
            if (product == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found");
            }
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository products, int id)
        {
            bool deleted = await products.DeleteAsync(id);
            if (!deleted)
            {
                return TypedResults.NotFound($"Product with id {id} not found");
            }
            return TypedResults.NoContent();
        }
    }
}
