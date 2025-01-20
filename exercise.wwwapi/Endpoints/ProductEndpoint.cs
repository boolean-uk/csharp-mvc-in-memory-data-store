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
            var pets = app.MapGroup("products");

            pets.MapGet("/{category?}", GetProduct);
            pets.MapGet("/", GetProductId);
            pets.MapGet("/", GetAllProducts);
            pets.MapPost("/", AddProduct);
            pets.MapDelete("/{id}", DeleteProduct);
            pets.MapPut("/{id}", UpdateProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllProducts(IRepository repository)
        {
            var students = repository.GetProducts();
            return Results.Ok(students);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> GetProduct(IRepository repository, [FromQuery] string? category)
        {
            var products = await repository.GetProduct(category);

            if (products == null || !products.Any())
            {
                return Results.NotFound("No products of the provided category were found.");
            }

            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> GetProductId(IRepository repository, [FromQuery] int id)
        {
            var products = await repository.GetProductId(id);

            if (products == null)
            {
                return Results.NotFound("No products of the provided Id were found.");
            }

            return TypedResults.Ok(products);
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductPost model)
        {
            try
            {
                if (model.Price != null || !string.IsNullOrEmpty(model.Name) || await repository.GetProductName(model.Name) != null)
                {
                    Product pet = new Product()
                    {
                        Name = model.Name,
                        Category = model.Category,
                        Price = model.Price
                    };
                    await repository.AddProduct(pet);

                    return TypedResults.Created($"https://localhost:7010/products/{pet.Id}", pet);
                }
                return Results.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exists");
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
                var model = await repository.GetProductId(id);
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
                var target = await repository.GetProductId(id);
                if (target == null) return Results.NotFound("Product not found");
                if (model.Name != null) target.Name = model.Name; else return Results.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exixst");
                if (model.Category != null) target.Category = model.Category; else return Results.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exixst");
                if (model.Price != null) target.Price = model.Price; else return Results.BadRequest("Price must be an integer, something else was provided. / Product with provided name already exixst");
                repository.UpdateProduct(target); 
                return Results.Ok(target);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

    }
}
