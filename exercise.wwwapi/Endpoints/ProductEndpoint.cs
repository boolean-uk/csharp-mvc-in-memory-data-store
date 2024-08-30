using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using exercise.wwwapi.ViewModels;
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
            var product = repository.Add(new Product(model.Name, model.Category, model.Price));
            if (product == null)
            {
                return TypedResults.BadRequest("Product already exists");
            }
            /*TODO: add 400 for price==int and name already exists*/
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetProducts(IRepository<Product> repository)
        {
            /*TODO: add category*/
            return TypedResults.Ok(repository.GetAll());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IRepository<Product> repository, string id)
        {
            var product = repository.Get(id);

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
            /*TODO: add 400 for price==int and name already exists*/
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
