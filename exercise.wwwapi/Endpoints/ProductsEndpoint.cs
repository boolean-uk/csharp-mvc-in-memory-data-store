using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductsEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapPost("/", AddProduct);
            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
            try
            {
                if (repository.GetProducts().Result.ToList().Any(p => p.Name == model.Name))
                    return TypedResults.BadRequest("Product with provided name already exists.");

                Product product = new Product()
                {
                    Name = model.Name,
                    Category = model.Category,
                    Price = model.Price
                };

                await repository.AddProduct(product);

                return TypedResults.Created($"https://localhost:7188/products/{product.Id}", product);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest("Not Found.");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository, string? category)
        {
            try
            {
                IEnumerable<Product> products = await repository.GetProducts();
                if (category == null)
                    return TypedResults.Ok(await repository.GetProducts());

                if (!products.ToList().Any(p => p.Category.ToLower() == category.ToLower()))
                    return TypedResults.NotFound("No products of the provided category were found.");

                return TypedResults.Ok(products.ToList().Where(p => p.Category == category));
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound("Not Found.");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            try
            {
                return TypedResults.Ok(await repository.GetProduct(id));
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound("Not Found.");
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut model)
        {
            try
            {
                var target = await repository.GetProduct(id);

                if (repository.GetProducts().Result.ToList().Any(p => p.Name == model.Name))
                    return TypedResults.BadRequest("Product with provided name already exists.");

                if (target == null) return TypedResults.NotFound();
                if (model.Name != null) target.Name = model.Name;
                if (model.Category != null) target.Category = model.Category;
                if (model.Price != null) target.Price = (int)model.Price;

                await repository.UpdateProduct(id);

                return TypedResults.Created($"https://localhost:7188/products/{target.Id}", target);
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound("Not found.");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            try
            {
                var model = await repository.GetProduct(id);
                if (await repository.DeleteProduct(id)) return TypedResults.Ok(new { When = DateTime.Now, Status = "Deleted", Id = model.Id, Name = model.Name, Category = model.Category, Price = model.Price });
                return TypedResults.NotFound("Not found.");
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
        }
    }
}
