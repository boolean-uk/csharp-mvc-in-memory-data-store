using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapGet("/{id}", GetAProduct);
            productGroup.MapDelete("{id}", DeleteProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository<Product> repository, int id)
        {
            if(!repository.GetAll().Any(x => x.Id == id))
            {
                return TypedResults.NotFound("Product not found");
            }
            var result = repository.Delete(id);
            return result != null ? TypedResults.Ok(result) : Results.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAllProducts(IRepository<Product> repository, [FromQuery] string? category)
        {
            if(!repository.GetAll().Any(x => x.Category == category)){
                return TypedResults.NotFound("No products of the provided category were found");
            }

            var allCategory = repository.GetAll().Where(x => x.Category == category);

            return TypedResults.Ok(allCategory);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAProduct(IRepository<Product> repository, int id)
        {
            if(!repository.GetAll().Any(x => x.Id == id))
            {
                return TypedResults.NotFound("Product not found");
            }
            return TypedResults.Ok(repository.Get(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository<Product> repository, ProductPost product)
        {
            if(repository.GetAll().Any(x => x.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Results.BadRequest("Product with provided name already exists");
            }

            var entity = new Product { Name=product.Name, Category=product.Category, Price=product.Price};
            repository.Add(entity);
            return TypedResults.Created($"/{entity.Id}", entity);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct (IRepository<Product> repository, int id, ProductPut product)
        {
            if (!repository.GetAll().Any(x => x.Id == id))
            {
                return TypedResults.NotFound("Product not found.");
            }
            var entity = repository.Get(id);

            if(product.Name != null)
            {
                if(repository.GetAll().Any(x => x.Name == product.Name))
                {
                    return Results.BadRequest("Product with with provided name already exists");
                }
            }

            entity.Name = product.Name != null ? product.Name : entity.Name;
            entity.Category = product.Category != null ? product.Category : entity.Category;
            entity.Price = product.Price != null ? product.Price : entity.Price;

            repository.Update(entity);
            return TypedResults.Created($"/{entity.Id}", entity);

        }
    }
}
