using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.ComponentModel;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapPost("/", CreateProduct);
            products.MapGet("/", GetAllProducts);
            products.MapGet("/{id}", GetaProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IRepository repository, ProductPostModel model)
        {

            Payload<Product> payload = new Payload<Product>();

            payload.data = repository.AddProduct(new Product() { Name = model.Name, Category = model.Category , Price = model.Price});



            return TypedResults.Ok(payload);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IRepository rep, string? category)
        {

                
                return TypedResults.Ok(rep.GetProducts(category));
 

    

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetaProduct(IRepository repository, int id)
        {
            if (repository.GetProduct(id) == null)
            {
                return TypedResults.NotFound("Product not found");
            }

            return TypedResults.Ok(repository.GetProduct(id));

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public static IResult UpdateProduct(IRepository repository, int id, ProductPostModel model)
        {
            Payload<Product> payload = new Payload<Product>();

            payload.data = repository.UpdateProduct(id, new Product() { Name = model.Name, Category = model.Category, Price = model.Price });

            if (repository.GetProduct(id) == null)
            {
                return TypedResults.NotFound("Product not found.");
            }

            return TypedResults.Ok(payload);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository repository, int id)
        {
            Payload<Product> payload = new Payload<Product>();

            payload.data = repository.DeleteProduct(id);
            if (repository.GetProduct(id) == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            return TypedResults.Ok(payload);
        }
    }
}
