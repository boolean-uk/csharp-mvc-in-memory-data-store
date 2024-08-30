

using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoint
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProductByID);
            products.MapPost("/", CreateProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetProducts(IRepository<Product> repository)
        {
            return TypedResults.Ok(repository.GetAll().ToList());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetProductByID(IRepository<Product> repository, int id)
        {
            return TypedResults.Ok(repository.Get(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static IResult CreateProduct(IRepository<Product> repository, Product product)
        {
            Payload<Product> payload = new Payload<Product>();
            payload.Data = repository.Create(product);

            return TypedResults.Ok(payload.Data);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        public static IResult UpdateProduct(IRepository<Product> repository, int id, Product product)
        {
            Payload<Product> payload = new Payload<Product>();
            payload.Data = repository.Update(id, product);

            return TypedResults.Ok(payload.Data);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult DeleteProduct(IRepository<Product> repository, int id)
        {
            return TypedResults.Ok(repository.Delete(id));
        }
    }
}
