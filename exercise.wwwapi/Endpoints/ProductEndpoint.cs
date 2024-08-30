using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapPost("/", CreateProduct);
            products.MapGet("/", GetAllProducts);
            products.MapGet("/{id}", GetAProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        public static Message responseMSG = new Message("Not found");

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IRepository repository, ProductPostModel model)
        {
            Product product = repository.CreateProduct(new Product() { Name = model.Name, Category = model.Category, Price = model.Price });

            return product != null ? TypedResults.Created("https://localhost:7188/products", product) : TypedResults.BadRequest(responseMSG);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IRepository repository, string? category)
        {
           
            List<Product> products = repository.GetAllProducts(category);
            return products != null ? TypedResults.Ok(products) : TypedResults.NotFound(responseMSG);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAProduct(IRepository repository, int id)
        {
            Product product = repository.GetAProduct(id);
            return product != null ? TypedResults.Ok(product) : TypedResults.NotFound(responseMSG);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IRepository repository, int id, ProductPostModel model)
        {
            Product product = repository.UpdateProduct(id, new Product() { Name = model.Name, Category = model.Category, Price = model.Price });

            return product != null ? TypedResults.Created($"https://localhost:7188/products/{id}", product) : TypedResults.BadRequest(responseMSG);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository repository, int id) 
        { 
            Product deleteProduct = repository.DeleteProduct(id);

            return deleteProduct != null ? TypedResults.Ok(deleteProduct) : TypedResults.NotFound(responseMSG);
        }
    }
}
