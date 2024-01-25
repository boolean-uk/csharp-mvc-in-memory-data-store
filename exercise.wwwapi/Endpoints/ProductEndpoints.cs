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

            productGroup.MapGet("/products", GetProducts);
            productGroup.MapPost("/products", AddProduct);
            productGroup.MapGet("/products/{id}", GetAProduct);
            productGroup.MapDelete("/products/{id}", DeleteProduct);
            productGroup.MapPut("/products/{id}", UpdateProduct);

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
        public static async Task<IResult> GetAProduct(IProductRepository productRepository, int id)
        {
            if (productRepository.GetAProduct(id) == default)
            {
                return TypedResults.NotFound("Product not found");
            }
            return TypedResults.Ok(productRepository.GetAProduct(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> AddProduct(IProductRepository productRepository, ProductPost product)
        {
            
            
            
            var newProduct = new Product() { Name = product.Name, Category = product.Category, Price = product.Price };
            productRepository.AddProduct(newProduct);         
            return TypedResults.Created($"/{newProduct.Id}", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(IProductRepository productRepository, int id)
        {
            if (productRepository.GetAProduct(id) == default)
            {
                return TypedResults.NotFound("Product not found");
            }
            return TypedResults.Ok(productRepository.DeleteProduct(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> UpdateProduct(IProductRepository productRepository, int id, ProductPut product)
        {

            return TypedResults.Ok(productRepository.UpdateProduct(id, product));
        }
    }
}
