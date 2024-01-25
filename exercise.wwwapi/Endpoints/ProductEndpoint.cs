using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var ProductGroup = app.MapGroup("products");

            ProductGroup.MapGet("/", GetProducts);
            ProductGroup.MapPost("/", AddProduct);
            ProductGroup.MapPut("/{id}", UpdateProduct);
            ProductGroup.MapGet("/{id}", GetAProduct);
            ProductGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IProductRepository repository, string? category)
        {
            if (category == null)
            {
                return TypedResults.Ok(repository.GetProducts());
            }
            if (!repository.GetProducts().Any(x => x.category == category))
            {
                return TypedResults.NotFound("No products of the provided category were found.");
            }
            else
            {
                return TypedResults.Ok(repository.GetProducts(category));
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAProduct(IProductRepository repository, int id)
        {
            if (id == null)
            {
                return Results.NotFound($"Product with id {id} was not found");
            }
            return TypedResults.Ok(repository.GetAProduct(id));
        }

     
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IProductRepository repository, ProductPost model)
        {
            if (!(model.price is int))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided.");
            }
            if (repository.GetProducts().Any(x => x.name == model.name))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }
            var newProduct = new Product() { name = model.name, category = model.category, price = model.price };
            repository.AddProduct(newProduct);
            return TypedResults.Created($"/{newProduct.Id}", newProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, int id, ProductPut name)
        {
           if(!(name.price is int))
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided.");
            }
            if (repository.GetProducts().Any(x => x.name == name.name))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }
            if (repository.GetAProduct(id) == null)
            {
                return TypedResults.NotFound($"Product not found.");
            }
                return TypedResults.Ok(repository.UpdateProduct(id, name));
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> DeleteProduct(IProductRepository repository, int id)
        {
            if (repository.GetAProduct(id) == null)
            {
                return TypedResults.NotFound($"Product not found.");
            }
            return TypedResults.Ok(repository.DeleteProduct(id));
        }




    }
}
