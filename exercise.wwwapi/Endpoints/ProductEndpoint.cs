using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using exercise.wwwapi.ViewModels;
using Microsoft.VisualBasic;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/GetAll", GetProductsByCategory);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", AddProduct);
            products.MapDelete("/{id}", DeleteProduct);
            products.MapPut("/{id}", UpdateProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductsByCategory(IProductRepository repository, string? category = null)
        {
            if (string.IsNullOrEmpty(category))
            {
                var allProducts = await repository.GetProducts();
                if (!allProducts.Any())
                {
                    return Results.NotFound("No products found");
                }
                return TypedResults.Ok(allProducts);

            }
            category = category.ToLower();
            var products = await repository.GetProductsByCategory(category);

            if (!products.Any())
            {
                return Results.NotFound("No products of the provided category were found");
            }

            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IProductRepository repository, int id)
        {
            var product = await repository.GetProduct(id);
            if (product == null)
            {
                return TypedResults.NotFound("Product not found");
            }
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IProductRepository repository, ProductPost model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.name))
                {
                    return TypedResults.BadRequest("The product name is required.");
                }
                if (string.IsNullOrEmpty(model.category))
                {
                    return TypedResults.BadRequest("The product category is required.");
                }

                if (!int.TryParse(model.price.ToString(), out int parsedPrice))
                {
                    return TypedResults.BadRequest("Price must be an integer, something else was provided");
                }

                if (model.price <= 0)
                {
                    return TypedResults.BadRequest("Prie can not be 0 or negative");
                }

                var checkExsisting = await repository.GetProducts();
                if (checkExsisting.Any(p => p.name.Equals(model.name)))
                {
                    return TypedResults.BadRequest("Product with provided name alredy exists.");
                }

                Product product = new Product()
                {
                    name = model.name,
                    category = model.category.ToLower(),
                    price = model.price
                };
                await repository.AddProduct(product);

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
        public static async Task<IResult> DeleteProduct(IProductRepository repository, int id)
        {
            try
            {
                var model = await repository.GetProduct(id);
                if (await repository.Delete(id)) return Results.Ok(new { When = DateTime.Now, Status = "Deleted", name = model.name, category = model.category, price = model.price });
                return TypedResults.NotFound("Product not found");
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, int id, ProductPut model)
        {
            try
            {
                var target = await repository.GetProduct(id);
                if (target == null) return Results.NotFound("Product not found");

                if (model.price != null && !int.TryParse(model.price.ToString(), out int parsedPrice))
                {
                    return TypedResults.BadRequest("Price must be an integer, something else was provided");
                }
                if (model.price <= 0)
                {
                    return TypedResults.BadRequest("Prie can not be 0 or negative");
                }
                var checkExsisting = await repository.GetProducts();
                if (!string.IsNullOrEmpty(model.name) && checkExsisting.Any(p => p.name.Equals(model.name)))
                {
                    return TypedResults.BadRequest("Product with provided name alredy exists.");
                }

                if (model.name != null) target.name = model.name;
                if (model.category != null) target.category = model.category.ToLower();
                if (model.price != null) target.price = model.price.Value;

                await repository.UpdateProduct(target);
                return Results.Ok(target);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
    }
}
