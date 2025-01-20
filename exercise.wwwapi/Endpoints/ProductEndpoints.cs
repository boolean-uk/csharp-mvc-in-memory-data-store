using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using exercise.wwwapi.View;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetAll);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", CreateProduct);
            products.MapDelete("/", DeleteProduct);
            products.MapPut("/", UpdateProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAll(IRepository rep, string? category)
        {
            var products = await rep.GetAll(category);
            return TypedResults.Ok(products);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository rep, string id)
        {
            try
            {
                var product = await rep.GetProduct(id);
                if(product == null) return TypedResults.NotFound("Product not found");

                return TypedResults.Ok(product);
            }
            catch (Exception ex) 
            {
                return TypedResults.Problem(ex.Message);
            }
            
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateProduct(IRepository rep, PostProduct model)
        {
            try
            {
                var product = new Product()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Category = model.Category,
                    Price = model.Price
                };

                if (model.Price.GetType() != typeof(int)) return TypedResults.Problem("Price must be an integer");

                await rep.CreateProduct(product);

                return TypedResults.Created($"https://localhost:7010/products/{product.Id}", product);
            }
            catch (Exception ex) 
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> DeleteProduct(IRepository rep, string id)
        {
            try
            {
                var model = await rep.GetProduct(id);
                if (model == null) return TypedResults.NotFound("Product not found");

                await rep.DeleteProduct(id);
                return Results.Ok(new { When = DateTime.Now, Status = "Deleted", Name = model.Name, Category = model.Category, Price = model.Price});

            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> UpdateProduct(IRepository rep, string id, PutProduct model)
        {
            try
            {
                var target = await rep.GetProduct(id);
                if (target == null) return Results.NotFound("Product not found");

                if (model.Name != null) target.Name = model.Name;
                if (model.Category != null) target.Category = model.Category;

                if (model.Price != null && model.Price.GetType() != typeof(int)) return TypedResults.Problem("Price must be an integer");
                else if (model.Price != null) target.Price = model.Price.Value;

                rep.Save();

                return Results.Ok(target);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
    }
}
