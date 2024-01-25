using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoint
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");


            productGroup.MapGet("", GetProducts);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPost("/{id}", AddProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, UserProduct userProduct)
        {
            if (!(userProduct.name.Any() &
                userProduct.price > 0 & 
                userProduct.category.Any())) return TypedResults.BadRequest("Bad input");

            if (repository.GetProducts().Count(x => x.name == userProduct.name) != 0)
                return TypedResults.BadRequest("Product already exists");

            Product product = repository.AddProduct(userProduct);
            return TypedResults.Created($"/{product.id}", product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            Product product = repository.GetProduct(id);
            if (product == null) return TypedResults.NotFound("Not found");
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository, string? category)
        {
            IEnumerable<Product> products = repository.GetProducts(category);
            return products.Count() == 0 ? TypedResults.NotFound("Not found") : TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, UserProduct userProduct)
        {
            Product product = repository.GetProduct(id);
            if (product == null) return TypedResults.NotFound("Not found");

            if (!(userProduct.name.Any() &
                userProduct.price > 0 &
                userProduct.category.Any())) return TypedResults.BadRequest("Bad input");

            if (repository.GetProducts().Count(x => x.name == userProduct.name) != 0)
                return TypedResults.BadRequest("Product already exists");

            repository.UpdateProduct(product, userProduct);
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            Product product = repository.GetProduct(id);
            if (product == null) return TypedResults.NotFound("Not found");

            repository.RemoveProduct(product);

            return TypedResults.Ok(product);
        }
    }
}
