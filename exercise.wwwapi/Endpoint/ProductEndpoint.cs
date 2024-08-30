

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProducts(IRepository<Product> repository)
        {
            if (repository.GetAll().ToList().Count() == 0) 
            {
                return TypedResults.NotFound("Not found");
            }

            return TypedResults.Ok(repository.GetAll().ToList());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProductByID(IRepository<Product> repository, int id)
        {
            if (repository.Get(id) == null)
            {
                return TypedResults.NotFound("Not Found");
            }

            return TypedResults.Ok(repository.Get(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IRepository<Product> repository, ProductPostModel product)
        {
            Payload<Product> payload = new Payload<Product>();

            if (!(product.Price is int))
            {
                return TypedResults.BadRequest("Price must be an integer");
            } 
            else if (repository.GetAll().Any(x => x.Name == product.Name))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }

            payload.Data = repository.Create(new Product() { 
                Name = product.Name, 
                Category = product.Category, 
                Price = product.Price 
            });

            return TypedResults.Ok(payload.Data);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IRepository<Product> repository, int id, Product product)
        {
            Payload<Product> payload = new Payload<Product>();

            if (repository.Get(id) == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            else if (!(product.Price is int))
            {
                return TypedResults.BadRequest("Price must be an integer");
            }
            else if (repository.GetAll().Any(x => x.Name == product.Name))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            } 

            payload.Data = repository.Update(id, product);

            return TypedResults.Ok(payload.Data);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository<Product> repository, int id)
        {
            if (repository.Get(id) == null)
            {
                return TypedResults.NotFound("Product not found.");
            }

            return TypedResults.Ok(repository.Delete(id));
        }
    }
}
