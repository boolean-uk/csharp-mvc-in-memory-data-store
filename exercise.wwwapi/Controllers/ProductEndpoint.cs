using exercise.wwwapi.Model;
using exercise.wwwapi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controllers
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var studentGroup = app.MapGroup("products");

            studentGroup.MapGet("/", GetProducts);
            studentGroup.MapGet("/{id}", GetProduct);
            studentGroup.MapPost("/", CreateProduct);
            studentGroup.MapPut("/{id}", UpdateProduct);
            studentGroup.MapDelete("/{id}", DeleteProduct);
        }

        //[ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public static async Task<IResult> GetProducts(Repository repository)
        {
            if (repository.GetProducts().Count() == 0)
                return TypedResults.NotFound();

            return TypedResults.Ok(repository.GetProducts());
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProduct(Repository repository, uint id)
        {
            var product = repository.GetProduct(id);
            if (product == default(Product))
                return TypedResults.NotFound();

            return TypedResults.Ok(product);
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> CreateProduct(Repository repository, Product product)
        {
            return TypedResults.Created("", repository.CreateProduct(product));
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> UpdateProduct(Repository repository, uint id, Product newProduct)
        {
            var prod = repository.GetProduct(id);
            if (prod == default(Product) || prod == null)
                return TypedResults.NotFound();


            return TypedResults.Ok(repository.UpdateProduct(id, newProduct));
        }

        [HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(Repository repository, uint id)
        {
            Product product = repository.GetProduct(id);
            if (product == null || product == default(Product))
                return TypedResults.NotFound("Not found.");

            repository.DeleteProduct(id);
            return TypedResults.Ok();
        }
    }
}
