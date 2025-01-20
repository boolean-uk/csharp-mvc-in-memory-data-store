using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;
using workshop.wwwapi.ViewModel;

namespace workshop.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var pets = app.MapGroup("pets");

            pets.MapGet("/", GetProduct);
            pets.MapPost("/", AddProduct);
            pets.MapDelete("/{id}", DeleteProduct);
            pets.MapPut("/{id}", UpdateProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProduct(IRepository repository)
        {
            var pets = await repository.GetProducts();
            return TypedResults.Ok(pets);
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
            try
            {

            Product pet = new Product()
            {
                Name = model.Name,
                Category = model.Category,
                Price = model.Price
            };
            await repository.AddProduct(pet);

            return TypedResults.Created($"https://localhost:7010/pets/{pet.Id}", pet);
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
                if (await repository.Delete(id)) return Results.Ok(new { When=DateTime.Now, Status="Deleted", Name=model.Name, Price=model.Price, Category=model.Category});
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
        public static async Task<IResult> UpdateProduct(IRepository repository, int id,  ProductPut model)
        {
            try
            {
                var target = await repository.GetProduct(id);
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
