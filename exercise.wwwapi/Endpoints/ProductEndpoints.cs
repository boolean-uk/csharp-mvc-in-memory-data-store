using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints( this WebApplication app)
        {
            var productGroup = app.MapGroup("Products");

            productGroup.MapGet("", GetProducts);
            productGroup.MapPost("", AddProduct);
            productGroup.MapGet("/{id}", GetAProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
            productGroup.MapPut("/{id}", UpdateProduct);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IProductRepository productRepository, string? category)
        {
            if (category == null)
            {
                return TypedResults.Ok(productRepository.GetAllProducts());

            }
            else if (!productRepository.GetProducts(category).Any())
            {
                return TypedResults.NotFound("No products of the provided category were found");
            }
            return TypedResults.Ok(productRepository.GetProducts(category));

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAProduct(IProductRepository productRepository, int id)
        {
            if (productRepository.GetAProduct(id) == default)
            {
                return TypedResults.NotFound("Product not found");
            }
            return TypedResults.Ok(productRepository.GetAProduct(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IProductRepository productRepository, ProductPost product)
        {
            if (productRepository.GetAllProducts().Any(x => x.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Results.BadRequest("Product with provided name already exists");
            }

            var newProduct = new Product() { Name = product.Name, Category = product.Category, Price = product.Price };
            productRepository.AddProduct(newProduct);         
            return TypedResults.Created($"/{newProduct.Id}", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository productRepository, int id)
        {
            if (!productRepository.GetAllProducts().Any(x => x.Id == id))
            {
                return TypedResults.NotFound("Product not found.");
            }
            var result = productRepository.DeleteProduct(id);
            return result != null ? TypedResults.Ok(result) : Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public static async Task<IResult> UpdateProduct(IProductRepository productRepository, int id, ProductPut product)
        {
            if (!productRepository.GetAllProducts().Any(x => x.Id == id))
            {
                return TypedResults.NotFound("Product not found.");
            }
            Product prod = productRepository.GetAProduct(id);

            if (product.Name != null)
            {
                if (productRepository.GetAllProducts().Any(x => x.Name == product.Name))
                {
                    return Results.BadRequest("Product with provided name already exists");
                }
            }
            prod.Name = product.Name != null ? product.Name : prod.Name;
            prod.Category = product.Category != null ? product.Category : prod.Category;
            prod.Price = product.Price != 0 ? product.Price : prod.Price;

            productRepository.UpdateProduct(id,prod);

            return TypedResults.Created($"/{prod.Id}", prod);

        }
    }
}
