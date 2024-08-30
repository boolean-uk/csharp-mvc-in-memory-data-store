using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapPost("/", AddProduct);
            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult AddProduct(IRepository<Product> repository, ProductPostModel model)
        {
            if (repository.GetByName(model.Name) != null)
            {
                return TypedResults.BadRequest($"Product named {model.Name} already exists.");
            }

            var product = repository.Add(new Product() { Name = model.Name, Category = model.Category, Price = model.Price });

            return TypedResults.Created($"/products", product);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProducts(IRepository<Product> repository, string? category)
        {
            List<Product> products = repository.GetAll(category);
            return TypedResults.Ok(products);                                                                           
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IRepository<Product> repository, int id)
        {
            Product product = repository.Get(id);

            return product != null ? TypedResults.Ok(product) : TypedResults.NotFound("Product not found");

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult UpdateProduct(IRepository<Product> repository, int id, ProductPostModel model)
        {
            Product product = repository.Update(id, new Product() { Name = model.Name, Category = model.Category, Price = model.Price });

            return product != null ? TypedResults.Created($"/products", product) : TypedResults.NotFound("Product not found");

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository<Product> repository, int id)
        {
            Product product = repository.Delete(id);

            return product != null ? TypedResults.Ok(product) : TypedResults.NotFound("Product not found");

        }
    }
}
