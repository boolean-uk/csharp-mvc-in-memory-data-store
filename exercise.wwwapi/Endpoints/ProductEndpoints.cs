
using System.Xml.Linq;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var products = app.MapGroup("/products");
            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", AddProduct).AddEndpointFilter<ValidationFilter<ProductPost>>();
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetProducts(IRepository repository)
        {
            var result = await repository.GetProducts();
            if (result == null) return Results.NotFound();
            return Results.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            var result = await repository.GetProduct(id);
            if (result == null) return Results.NotFound(new { message = "Not found" });
            return Results.Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
            if (model.name == null || model.category == null || model.price == null) return Results.BadRequest(new { message = "Not found" });
            Product product = new Product()
            {
                name = model.name,
                category = model.category,
                price = (int)model.price
            };
            product = await repository.AddProduct(product);
            return Results.Created($"https://localhost:7009/products/{product.Id}", product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut model)
        {
            try
            {
                if (model.name == null && model.category == null && model.price == null) return Results.BadRequest(new { message = "Not found" });
                Product product = await repository.UpdateProduct(id, model);
                if (product == null) return Results.NotFound(new { message = "Not found" });
                return Results.Created($"https://localhost:7009/products/{product.Id}", product);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            try
            {
                var target = await repository.GetProduct(id);
                if (await repository.DeleteProduct(id)) return Results.Ok(new { id = target.Id, name = target.name, category = target.category, price = target.price });
                return Results.NotFound(new { message = "Not found" });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }


    }
}
