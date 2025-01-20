using exercise.wwwapi.Interface;
using exercise.wwwapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoint
{
    public static class ProductsEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetSingleProduct);
            products.MapPost("/", AddProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IProductRepository repository, [FromQuery] string? category = null)
        {
            var products = await repository.GetProducts(); //fetch all products

            if (string.IsNullOrEmpty(category)) // if category isnt provided in json payload
            {
                return products.Any() // if there are any products
                    ? Results.Ok(products) // return 200 OK with all products
                    : Results.NotFound(new { message = "No products found." }); // return 404 Not Found if no products exists at all
            }

            // filterdProducts = Products with the category provided (case insensitive)
            var filteredProducts = products
                .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return filteredProducts.Any() // if there are products with the provided category in json payload
                ? Results.Ok(filteredProducts) // return 200 OK with all products with given category
                : Results.NotFound(new { message = "No products of the provided category were found." }); // self-explanatory
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetSingleProduct(IProductRepository repository, int id)
        {
            var product = await repository.GetSingleProduct(id);
            if (product == null)
            {
                return Results.NotFound(new { message = "Not found." });
            }
            return Results.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> AddProduct(IProductRepository repository, ProductDTO product)
        {
            if (product.Price < 0)
            {
                return Results.BadRequest(new { message = "Price must be a positive integer." });
            }

            var existingProduct = await repository.GetProductByName(product.Name);
            if (existingProduct != null)
            {
                return Results.BadRequest(new { message = "Product with provided name already exists." });
            }

            var createdProduct = await repository.AddProduct(new Product
            {
                Name = product.Name,
                Category = product.Category,
                Price = product.Price
            });

            return Results.Created($"/products/{createdProduct.Id}", createdProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, int id, ProductDTO product)
        {
            if (product.Price < 0)
            {
                return Results.BadRequest(new { message = "Price must be a positive integer" });
            }

            var existingProduct = await repository.GetSingleProduct(id);
            if (existingProduct == null)
            {
                return Results.NotFound(new { message = "Not found." });
            }

            var existingProductName = await repository.GetProductByName(product.Name);
            if (existingProductName != null)
            {
                return Results.BadRequest(new { message = "Product with provided name already exists." });
            }

            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;

            var updatedProduct = await repository.UpdateProduct(id, existingProduct);

            return Results.Created($"/products/{id}", updatedProduct);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(IProductRepository repository, int id)
        {
            var product = await repository.GetSingleProduct(id);
            if (product == null)
            {
                return Results.NotFound(new { message = "Not found." });
            }

            await repository.DeleteProduct(id);
            return Results.Ok(product);
        }
    }
}
