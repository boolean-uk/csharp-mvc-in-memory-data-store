using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Viewmodel;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", AddProduct);
            products.MapDelete("/{id}", DeleteProduct);
            products.MapPut("/{id}", UpdateProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IRepository repository, string category = "")
        {
            var Products = await repository.GetProducts();
            if (!(category == ""))
            {
                Products = await repository.GetProducts(category);
                if(Products.Count() == 0)
                {
                    return TypedResults.NotFound("No product of the provided category was found");
                }
            }

            return TypedResults.Ok(Products);
        }
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            var Product = await repository.GetProduct(id);
            if (Product == null)
                return TypedResults.NotFound("Product was not found");

            return TypedResults.Ok(Product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
            if (!int.TryParse(model.Price.ToString(), out int price))
                return TypedResults.BadRequest("Price must be a valid integer");
            try
            {

                var products = await repository.GetProducts();
                var amount = products.ToList().FirstOrDefault(p => p.Name == model.Name);
                if (!(amount == null))
                {
                    return TypedResults.BadRequest("A product with that name already exists");
                }
                Products product = new Products()
                {
                    Name = model.Name,
                    Category = model.Category,
                    Price = model.Price,
                };
                await repository.AddProduct(product);

                return TypedResults.Created($"https://localhost:7188/products/{product.Id}", product);



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
                var model = await repository.GetProduct(id);
                if (model == null)
                    return TypedResults.NotFound("Product was not found");
                if (await repository.DeleteProduct(id)) return Results.Ok(new { When = DateTime.Now, Status = "Deleted", Name = model.Name, Category = model.Category, Price = model.Price });
                return TypedResults.NotFound();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPut model)
        {
            try
            {
                var target = await repository.GetProduct(id);
                if (target == null) return TypedResults.NotFound("Product was not found");
                var products = await repository.GetProducts();
                var amount = products.ToList().FirstOrDefault(p => p.Name == model.Name);
                if (amount != null && amount.Id != id)
                    return TypedResults.BadRequest("A product with that name already exists");

                if (model.Name != null) target.Name = model.Name;
                if (model.Category != null) target.Category = model.Category;
                if (model.Price != null) target.Price = model.Price.Value;
                var result = await repository.UpdateProduct(target);
                return Results.Created($"https://localhost:7188/products/{id}", result);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
    }
}
