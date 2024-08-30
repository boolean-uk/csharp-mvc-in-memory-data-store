using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", AddProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult AddProduct(IRepository<Product> repository, ProductPostModel model)
        {
            if (!int.TryParse(model.Price.ToString(), out var productId))
            {
                /*TODO FIX Price must be and integer*/
                return TypedResults.BadRequest($"Price {model.Price} must be an integer, something else was provided.");
            }

            if (repository.GetByName(model.Name) is not null)
            {
                return TypedResults.BadRequest($"Product with provided name {model.Name} already exists.");
            }

            var product = repository.Add(new Product(model.Name, model.Category, model.Price));
            if (product == null)
            {
                return TypedResults.BadRequest("Product already exists");
            }
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProducts(IRepository<Product> repository, string category)
        {
            var products = repository.GetAll(category);

            if (!string.IsNullOrEmpty(category) && products.Count == 0)
            {
                return TypedResults.NotFound($"No products of the provided category {category} were found.");
            }
            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IRepository<Product> repository, string id)
        {
            var product = repository.GetById(id);

            if (product == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found.");
            }

            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IRepository<Product> repository, string id, ProductPostModel model)
        {
            if (!int.TryParse(model.Price.ToString(), out var productId))
            {
                /*TODO FIX Price must be and integer*/
                return TypedResults.BadRequest($"Price {model.Price} must be an integer, something else was provided.");
            }

            if (repository.GetByName(model.Name) is not null)
            {
                return TypedResults.BadRequest($"Product with provided name {model.Name} already exists.");
            }
            
            var product = repository.Update(id, new Product(model.Name, model.Category, model.Price));
            if (product == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found.");
            }
            return TypedResults.Created($"https://localhost:7068/books/{product.Id}", product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository<Product> repository, string id)
        {
            var product = repository.Delete(id);

            if (product == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found.");
            }

            return TypedResults.Ok(product);
        }
    }
}
