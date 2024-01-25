using exercise.wwwapi.Data;
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
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct); 
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, Product model)
        {
            var foundProduct = repository.GetProductByName(model.Name); 
            if (foundProduct != null) 
            {
                return TypedResults.BadRequest($"Product {model.Name} already exists."); 
            }
            else
            {
                var productToAdd = new Product() { Name = model.Name, Category = model.Category, Price = model.Price };
                repository.AddProduct(productToAdd);
                return TypedResults.Ok(productToAdd);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository, string? category)
        {
            if(category != null) 
            {
                if(!repository.GetProductsByCategory(category).Any())
                {
                    return TypedResults.NotFound("Not found.");
                }
                else
                {
                    return TypedResults.Ok(repository.GetProductsByCategory(category));
                }
            }
            else
            {
                return TypedResults.Ok(repository.GetProducts());
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            var foundProduct = repository.GetProduct(id);
            if (foundProduct == null)
            {
                return TypedResults.NotFound("Not found.");
            }
            else
            {
                return TypedResults.Ok(foundProduct);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, string newName, string newCategory, int newPrice)
        {
            var foundProduct = repository.GetProductByName(newName);
            if (foundProduct != null) 
            {
                return TypedResults.BadRequest($"Product {newName} already exists.");
            }
            var productToUpdate = repository.GetProduct(id);
            if (productToUpdate == null)
            {
                return TypedResults.NotFound("Not found.");
            }
            else
            {
                var updatedProduct = repository.UpdateProduct(id, newName, newCategory, newPrice);
                return TypedResults.Ok(updatedProduct);

            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            var foundProduct = repository.GetProduct(id); 
            if (foundProduct == null) 
            {
                return TypedResults.NotFound("Not found.");
            }
            var productToDelete = repository.DeleteProduct(id);
            return TypedResults.Ok(productToDelete);
        }
    }
}
