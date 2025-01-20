using System.Reflection.Metadata.Ecma335;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapPost("/", AddProduct);
            products.MapGet("/{id}", GetProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IRepository repository, string category)
        {
            var products = await repository.GetAll(category);
            if (products.Count() == 0) return TypedResults.NotFound("No products of the provided category were found");
            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            var product = await repository.Get(id);
            if (product == null) return TypedResults.NotFound("Product not found.");
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductViewModel model)
        {
            if (model.price <= 0) return TypedResults.BadRequest("Price must be more than zero (0).");
            if (repository.NameExists(model.name, 0).Result) return TypedResults.BadRequest("Product with provided name already exists");
            try
            {
                Product product = new Product()
                {
                    name = model.name,
                    category = model.category,
                    price = model.price
                };
                await repository.Add(product);

                return TypedResults.Created($"https://localhost:7010/products/{product.Id}", product);
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
                var model = await repository.Get(id);
                if (model == null) return TypedResults.NotFound("Product not found.");
                if (await repository.Delete(id)) return TypedResults.Ok(model);
                return TypedResults.NotFound("Product not found.");
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductViewModel model)
        {
            try
            {
                if (model.price <= 0) return TypedResults.BadRequest("Price must be more than zero (0).");
                var target = await repository.Get(id);
                var alreadyExists = await repository.NameExists(model.name, id);
                if (target == null) return TypedResults.NotFound("Product not found.");
                if (alreadyExists) return TypedResults.BadRequest("Product with provided name already exists");
                if (model.name != "" && model.price != 0 && model.category != "")
                {
                    target.name = model.name;
                    target.price = model.price;
                    target.category = model.category;
                    repository.Update(target);
                    return TypedResults.Ok(model);
                }
                return TypedResults.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exists");
                
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

    }
}
