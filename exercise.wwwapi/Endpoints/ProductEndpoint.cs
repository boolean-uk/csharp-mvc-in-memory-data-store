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

            productGroup.MapPost("/", AddProduct);

            productGroup.MapGet("/", GetProducts);

            productGroup.MapGet("/{id}", GetAProduct);

            productGroup.MapPut("/{id}", UpdateProduct);

            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost product)
        {
            if (!int.TryParse(product.Price, out var price))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided");
            }

            var newProduct = new Product() { Name = product.Name, Category = product.Category.ToLower(), Price = price };
            newProduct = repository.AddProduct(newProduct);

            if (newProduct == null)
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }

            return TypedResults.Created("/", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IRepository repository, string category)
        {
            var products = repository.GetProducts(category.ToLower());

            if (products.Count() == 0)
            {
                return TypedResults.NotFound("No products of the provided category were found");
            }

            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAProduct(IRepository repository, int id)
        {
            var product = repository.GetAProduct(id);

            if (product == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPost product)
        {
            if (!int.TryParse(product.Price, out _))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided");
            }

            var newProduct = repository.UpdateProduct(id, product);

            if (newProduct == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            if (newProduct.Name == null)
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }

            return TypedResults.Created("/", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            var product = repository.DeleteProduct(id);

            if (product == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            return TypedResults.Ok(product);
        }
    }
}
