using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var ProductGroup = app.MapGroup("products");

            ProductGroup.MapGet("/", Get);
            ProductGroup.MapPost("/", Add).AddEndpointFilter(async (invocationContext, next) =>
            {
                var product = invocationContext.GetArgument<Product>(1);

                if (string.IsNullOrEmpty(product.Name) || string.IsNullOrEmpty(product.Category))
                {
                    // not part of extension spec, but validates input
                    return Results.BadRequest("You must enter a Name AND Category");
                }
                return await next(invocationContext);
            }); ;
            ProductGroup.MapGet("/{id}", GetById);
            ProductGroup.MapPut("/{id}", Update);
            ProductGroup.MapDelete("/{id}", Delete);
        }
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> Get(IRepository<Product> repository, string? Category)
        {
            if (Category != null && !repository.Get().Any(x => x.Category == Category))
            {
                return TypedResults.NotFound("No products of the provided category were found");
            }
            if (Category == null) // if no category provided, search all products anyway
            {
                return TypedResults.Ok(repository.Get());
            }
            var result = repository.Get().Where(x=>x.Category == Category);
            return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> Add(IRepository<Product> repository, Product product)
        {
            bool priceInt = int.TryParse(product.Price.ToString(), out var price);
            if (priceInt == false)
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided");
            }
            if (product.Name != null)
            {
                if (repository.Get().Any(x => x.Name == product.Name))
                {
                    return TypedResults.BadRequest("Product with provided name already exists");
                }
            }
            return TypedResults.Ok(repository.Add(product));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetById(IRepository<Product> repository, int id)
        {
            if (!repository.Get().Any(x => x.Id == id))
            {
                return TypedResults.NotFound("Product not found");
            }
            return TypedResults.Ok(repository.GetById(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound) ]
        public static async Task<IResult> Update(IRepository<Product> repository, int id, ProductPut product)
        {
            if (!repository.Get().Any(x => x.Id == id))
            {
                return TypedResults.NotFound("Product not found");
            }
            var entity = repository.GetById(id);
            bool priceInt = int.TryParse(product.Price.ToString(), out var price);
            if (priceInt == false)
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided");
            }
            if (product.Name != null)
            {
                if (repository.Get().Any(x => x.Name == product.Name))
                {
                    return TypedResults.BadRequest("Product with name already exists");
                }
            }
            entity.Name = product.Name != null ? product.Name : entity.Name;
            entity.Price = (int)(product.Price != null ? product.Price : entity.Price);
            entity.Category = product.Category != null ? product.Category : entity.Category;

            repository.Update(entity);

            return TypedResults.Created($"/{entity.Id}", entity);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Delete(IRepository<Product> repository, int id)
        {
            if (!repository.Get().Any(x => x.Id == id))
            {
                return TypedResults.NotFound("Product not found");
            }
            var result = repository.Delete(id);
            return result != null ? TypedResults.Ok(result) : TypedResults.NotFound();
        }
    }
}
