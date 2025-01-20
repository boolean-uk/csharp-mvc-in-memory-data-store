using exercise.wwwapi.DTO;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductsEndpoint
    {
        public static void ConfigureProductsEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            app.MapPost("/", AddProduct).AddEndpointFilter<ValidationFilter<ProductDto>>();
            app.MapGet("/{id}", GetProduct);
            app.MapGet("/", GetProducts);
            app.MapPut("/{id}", UpdateProduct).AddEndpointFilter<ValidationFilter<ProductDto>>();
            app.MapDelete("/{id}", DeleteProduct);

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductDto model)
        {
            try
            {
                Product product = new Product();
                product.Name = model.Name;
                product.Category = model.Category;
                product.Price = model.Price;
                await repository.Add(product);

                return TypedResults.Created($"https://localhost:7188/products/", product);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository, string? category = null)
        {
            try
            {
               var products = await repository.GetAll(category);
               return products.Any() ? TypedResults.Ok(products) : TypedResults.NotFound();
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            try
            {
                var product = await repository.Get(id);
                return product != null ? TypedResults.Ok(await repository.Get(id)) : TypedResults.NotFound();
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductDto model)
        {
            try
            {
                var target = repository.Get(id).Result;
                if (target == null) return Results.NotFound();
                if (model.Name != null) target.Name = model.Name;
                if (model.Category != null) target.Category = model.Category;
                if (model.Price != null) target.Price = model.Price;

                return await repository.Update(target) != null ? Results.Created($"https://localhost:7188/products/{target.Id}", target) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            try
            {
                var product = await repository.Delete(id);
                return product != null ? Results.Ok(product) : TypedResults.NotFound();
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }

    }
}
