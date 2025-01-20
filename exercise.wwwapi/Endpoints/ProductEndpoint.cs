using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetOne);
            products.MapPost("/", AddProducts);
            products.MapDelete("/{id}", DeleteProducts);
            products.MapPut("/{id}", UpdateProducts);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IRepository repository, string category = "None")
        {
            var pets = await repository.GetAll(category);

            return TypedResults.Ok(pets);
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public static async Task<IResult> GetOne(IRepository repository, int id)
        {
            try 
            {
                var book = await repository.Get(id);
                return TypedResults.Ok(book);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public static async Task<IResult> AddProducts(IRepository repository, ProductPost model)
        {
            try
            {

                Product pet = new Product()
                {
                    Name = model.Name,
                    Category = model.Category,
                    Price = model.Price
                };
                await repository.Add(pet);

                return TypedResults.Created($"https://localhost:7010/pets/{pet.Name}", pet);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProducts(IRepository repository, int id)
        {
            try
            {
                var model = await repository.Get(id);
                if (await repository.Delete(id)) return Results.Ok(new { When = DateTime.Now, Status = "Deleted", Name = model.Name, Category = model.Category, Price = model.Price });
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
        public static async Task<IResult> UpdateProducts(IRepository repository, int id, ProductPut model)
        {
            try
            {
                var target = await repository.Get(id);
                if (target == null) return Results.NotFound();
                if (model.Name != null) target.Name = model.Name;
                if (model.Category != null) target.Category = model.Category;
                if (model.Price != null) target.Price = model.Price.Value;
                return Results.Ok(target);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }
    }
}
