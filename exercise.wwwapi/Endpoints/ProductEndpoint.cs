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
            products.MapGet("/", GetAllProducts);
            products.MapGet("{id}", GetProduct);
          //  products.MapPost("/", CreateProduct);
           // products.MapPut("{id}", UpdateProduct);
            products.MapDelete("{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IRepository<Product> repository, string category)
        {
            List<Product> products = repository.GetAllProducts(category);

            if (products == null || products.Count == 0)
            {
                return TypedResults.NotFound("No products of the provided category were found");
            }
            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static IResult GetProduct(IRepository<Product> repository, int id)
        {
            Product product = repository.Get(id);

            if (product != null)
            {
                return TypedResults.Ok(product);
            }
            return TypedResults.NotFound("Product not found");

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static IResult DeleteProduct(IRepository<Product> repository, int id)
        {
            if (repository.Get(id) == null)
            {
                return TypedResults.NotFound();

            }
            return TypedResults.Ok(repository.Delete(id));

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public static IResult CreateProduct(IRepository<Product> repository, ProductPostModel model)
        {
            Payload<Product> payload = new Payload<Product>();

            if(!(model.Price is int))
            {
                return TypedResults.BadRequest("Price Must be an integer");
            }

            payload.data = repository.Create(new Product()
            {
                Name = model.Name,
                Category = model.Category,
                Price = model.Price,
            });
            return TypedResults.Ok(payload);
        }
    }

}
