using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureCarEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapGet("/", GetProduct);  
            productGroup.MapPost("/", AddProduct);
            productGroup.MapGet("/{id}", GetProductByID);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);

        }

        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {

            return TypedResults.Ok(repository.DeleteProduct(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut model)
        {
            return TypedResults.Ok(repository.UpdateProduct(id, model));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProductByID(IRepository repository, int id)
        {
            return TypedResults.Ok(repository.GetProductByID(id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProduct(IRepository repository)
        {
            return TypedResults.Ok(repository.GetAllProducts());
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
            if (model == null)
            {

            }
            var newProduct = new Product() { Id = model.Id, Category = model.Category, Name = model.Name, Price = model.Price };
            repository.AddProduct(newProduct);
            return TypedResults.Created($"/{newProduct.Id}", newProduct);
        }
    }
}
