using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Model;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
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
        public static IResult GetAllProducts(IProductRepository products)
        {
            return TypedResults.Ok(products.GetAll());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProductById(IProductRepository products, int id)
        {
            Product? product = products.GetById(id);
            if (product == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found");
            }
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IProductRepository products, ProductCreatePayload payload)
        {
            Product product = new Product
            {
                Name = payload.Name,
                Category = payload.Category,
                Price = payload.Price
            };
            products.Add(product);
            return TypedResults.Created($"/products/{product.Id}", product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IProductRepository products, int id, ProductUpdatePayload payload)
        {
            Product? product = products.Update(id, payload);
            if (product == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found");
            }
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IProductRepository products, int id)
        {
            bool deleted = products.Delete(id);
            if (!deleted)
            {
                return TypedResults.NotFound($"Product with id {id} not found");
            }
            return TypedResults.NoContent();
        }
    }
}
