
using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapPost("/", CreateProduct);
            productGroup.MapGet("/", AllProducts);
            productGroup.MapGet("/{id}", Product);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
            productGroup.MapGet("/seed", SeedProducts);

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> CreateProduct(IRepository<Product> repository, ProductBody productBody)
        {
            if (repository.Get().Any(x => x.Name.Equals(productBody.Name, StringComparison.OrdinalIgnoreCase)))
            {
                return Results.BadRequest("Product with provided name already exists");
            }
            if (productBody.Price <= 0)
            {
                return Results.BadRequest("Price must be a positive integer");
            }
            Product product = new Product()
            {
                Id = repository.Get().Any() ? repository.Get().Max(b => b.Id) + 1 : 1,
                Name = productBody.Name,
                Price = productBody.Price,
                Category = productBody.Category
            };
            repository.Insert(product);
            return TypedResults.Created(product.Id.ToString(), product);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> AllProducts(IRepository<Product> repository)
        {
            if (!repository.Get().Any()) return Results.BadRequest("Not Found");
            return TypedResults.Ok(repository.Get());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> Product(IRepository<Product> repository, int id)
        {
            var product = repository.GetById(id);
            if (product == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> UpdateProduct(IRepository<Product> repository, int id, ProductBody productBody)
        {
            var existingProduct = repository.Get().FirstOrDefault(x => x.Id == id);
            if (existingProduct == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            if (repository.Get().Any(x => x.Name.Equals(productBody.Name, StringComparison.OrdinalIgnoreCase) && x.Id != id))
            {
                return Results.BadRequest("Product with provided name already exists");
            }
            if (productBody.Price <= 0)
            {
                return Results.BadRequest("Price must be a positive integer");
            }
            existingProduct.Name = productBody.Name;
            existingProduct.Price = productBody.Price;
            existingProduct.Category = productBody.Category;
            repository.Update(existingProduct);

            return TypedResults.Created($"products/{existingProduct.Id}", existingProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> DeleteProduct(IRepository<Product> repository, int id)
        {
            if (!repository.Get().Any(x => x.Id == id))
            {
                return TypedResults.NotFound("Car not found.");
            }
            var result = repository.Delete(id);
            return result != null ? TypedResults.Ok(result) : Results.NotFound();
        }

        private static async Task<IResult> SeedProducts(IRepository<Product> repository, IColl<Product> productHelper)
        {
            List<Product> products = productHelper.GetAll().ToList();         
            products.ForEach(product => repository.Insert(product));
            return Results.Ok("Seeded database");
        }
    }
}

