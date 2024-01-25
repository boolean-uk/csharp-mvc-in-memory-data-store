using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace workshop.wwwapi.Endpoints
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository, string? category)
        { 
            if (category == null)
            {
                return TypedResults.Ok(repository.GetProducts());
            }
            if (!repository.GetProducts().Any(x => x.Category == category))
            {
                return TypedResults.NotFound("No products of the provided category were found.");
            } else
            {
                return TypedResults.Ok(repository.GetProducts(category));
            }
            
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAProduct(IRepository repository, int id)
        {
            var product = repository.GetAProduct(id);
            if (product == null)
            {
                return TypedResults.NotFound($"Product not found.");
            }
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
            if (!(model.Price is int))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided.");
            }
            if (repository.GetProducts().Any(x => x.Name == model.Name))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }
            var newProduct = new Product() { Name = model.Name, Category = model.Category, Price = model.Price };
            repository.AddProduct(newProduct);
            return TypedResults.Created($"/{newProduct.Id}", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut model)
        {
            if(!(model.Price is int))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided.");
            }
            if (repository.GetProducts().Any(x => x.Name == model.Name && x.Id != id))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }
            if (repository.GetAProduct(id) == null)
            {
                return TypedResults.NotFound($"Product not found.");
            }
            return TypedResults.Ok(repository.UpdateProduct(id, model));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            if(repository.GetAProduct(id) == null)
            {
                return TypedResults.NotFound($"Product not found.");
            }
            return TypedResults.Ok(repository.DeleteProduct(id));
        }

    }
}
