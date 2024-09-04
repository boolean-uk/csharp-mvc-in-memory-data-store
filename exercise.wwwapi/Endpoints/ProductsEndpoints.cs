using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using exercise.wwwapi.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var product = app.MapGroup("Product");
            product.MapGet("/", GetProducts);
            product.MapPost("/", AddProduct);
            product.MapGet("/{id}", GetProduct);
            product.MapPut("/{id}", UpdateProduct);
            product.MapDelete("/{id}", DeleteProduct);

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProducts(IRepository repository)
        {
            if (repository.IsEmpty())
                return TypedResults.BadRequest("No products of the provided category were found");

            Payload<List<Product>> payload = new Payload<List<Product>>();
            payload.data = repository.GetProducts();


            return TypedResults.Ok(payload);
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult AddProduct(IRepository repository, ProductPostModel model)
        {

            Payload<Product> payload = new Payload<Product>();
            if (repository.ContainsProduct(model.Name))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided. /Product with provided name already exists.");
            }
            payload.data = repository.AddProduct(new Product() { Name = model.Name, Category = model.Category, Price = model.Price });

            return TypedResults.Created();
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IRepository repository, int id)
        {
            var result = repository.GetProduct(id);

            return repository.ContainsProduct(id) ? TypedResults.Ok(result) : TypedResults.NotFound("Product not found");
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IRepository repository, ProductPostModel postmodel, int id)
        {
            if (!repository.ContainsProduct(id))
            {
                return TypedResults.NotFound("Product not found");
            }
            else if (repository.ContainsProduct(postmodel.Name))
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }
                
            var result = repository.ChangeProduct(postmodel, id);

            return TypedResults.Created();
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository repository, int id)
        {
            if (!repository.ContainsProduct(id))
                return TypedResults.NotFound("Product Not Found");
            var result = repository.DeleteProduct(id);

            return TypedResults.Ok(result);
        }
    }
}