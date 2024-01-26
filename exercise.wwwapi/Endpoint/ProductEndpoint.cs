using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace exercise.wwwapi.Endpoint
{
    // This is the controller in the application
    // A controller class might validate user input before before telling the Model layer to do whatever in database
    // The controller should notify the view when the action is done
    public static class ProductEndpoint
    {

        public static void ConfigureEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetProduct);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
            productGroup.MapPut("/{id}", UpdateProduct);

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllProducts(IRepository repository)
        {
            return TypedResults.Ok(repository.GetAllProducts());
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            return TypedResults.Ok(repository.GetProductById(id));
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> AddProduct(IRepository repository, Product product)
        {
            var prod = repository.CreateProduct(product);
            return TypedResults.Created($"{prod.name} was added");
        }

        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            return TypedResults.Ok(repository.DeleteProductById(id));
        }

        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut productPut)
        {
            return TypedResults.Ok(repository.UpdateProductById(id, productPut));
        }
    }
}
