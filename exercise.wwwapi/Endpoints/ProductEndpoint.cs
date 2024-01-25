using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapPost("/", CreateProduct);
            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id}", GetAProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IProductRepository repository, string category = null)
        {
            return TypedResults.Ok(repository.GetProducts(category));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAProduct(IProductRepository repository, int id)
        {
            return TypedResults.Ok(repository.GetAProduct(id));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreateProduct(IProductRepository repository, ProductPost model)
        {
            //validate
            if (model == null)
            {

            }

            int newId = 1;
            if(repository.GetProducts().Any())
            {
                newId = repository.GetProducts().Max(product => product.Id) + 1;
            }


            var newProduct = new Product() { Id = newId, Name = model.Name, Category = model.Category, Price = model.Price };
            repository.CreateProduct(newProduct);
            return TypedResults.Created($"/{newProduct.Id}", newProduct);
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, int id, ProductPut model)
        {
            return TypedResults.Ok(repository.UpdateProduct(id, model));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> DeleteProduct(IProductRepository repository, int id)
        {
            Product deleteThis = repository.DeleteProduct(id);
            if (deleteThis != null)
            {
                return TypedResults.Ok(deleteThis);
            }
            else
            {
                return TypedResults.NotFound($"Product with that ID does not exist.");
            }
        }
    }
}
