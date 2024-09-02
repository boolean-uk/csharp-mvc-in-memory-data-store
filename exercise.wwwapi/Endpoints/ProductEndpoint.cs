using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureCarEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetAProduct);
            products.MapPost("/", AddProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProducts(IRepository<Product> repository, string? category)
        {
            Payload<List<Product>> payload = new Payload<List<Product>>();
            payload.data = repository.GetAll(category);
            return payload.data != null ? TypedResults.Ok(payload) : TypedResults.NotFound(new Message());
        }

        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAProduct(IRepository<Product> repository, int id)
        {
            Payload<Product> payload = new Payload<Product>();
            payload.data = repository.GetOne(id);
            return payload.data != null? TypedResults.Ok(payload) : TypedResults.NotFound(new Message());
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static IResult AddProduct(IRepository<Product> repository, ProductPostModel model)
        {
            Payload<Product> payload = new Payload<Product>();
            payload.data = repository.Add(new Product() { name = model.name, category = model.category, price = model.price });
            return TypedResults.Created<Payload<Product>>("", payload);
        }

        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IRepository<Product> repository, int id, ProductPostModel model)
        {
            Payload<Product> payload = new Payload<Product>();
            payload.data = repository.Update(id, new Product() { name = model.name, category = model.category, price = model.price });

            return payload.data != null ? TypedResults.Created("", payload) : TypedResults.NotFound(new Message());
        }

        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository<Product> repository, int id)
        {
            Payload<Product> payload = new Payload<Product>();
            payload.data = repository.Delete(id);

            return payload.data != null? TypedResults.Ok(payload) : TypedResults.NotFound(new Message()); 
        }
    }
    
}
