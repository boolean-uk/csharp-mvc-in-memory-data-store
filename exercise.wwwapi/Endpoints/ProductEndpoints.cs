
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints {

    public static class ProductEndPoints {

        public static void ConfigureProductsEndpoint(this WebApplication app) {
            var students = app.MapGroup("products");
            students.MapGet("/", GetAllProducts);
            students.MapPost("/", CreateProduct);
            students.MapGet("/{Id}", GetProduct);
            students.MapPut("/{Id}", UpdateProduct);
            students.MapDelete("/{Id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateProduct(IProductRepository sr, ProductPostPayload payload)
        {
            if (payload.Name == null)
                return Results.BadRequest(new { Message = "Name cannot be null" });
            if(payload.Category == null)
                return Results.BadRequest(new { Message = "Category cannot be null" });
            if(payload.Price == 0)
                return Results.BadRequest(new { Message = "Price cannot be null" });
            Product product = await sr.AddProduct(payload.Name, payload.Category, payload.Price);
            return TypedResults.Created($"/products/{product.Name}", product);
        }

        public static async Task<IResult> GetAllProducts(IProductRepository sr)
        {
            return TypedResults.Ok(await sr.GetAllProducts());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IProductRepository sr, int Id)
        {
            var language = await sr.GetProduct(Id);

            if (language == null)
            {
                return Results.NotFound(new { Message = $"Product with Id {Id} not found."});
            }

            return TypedResults.Ok(language);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository sr, int Id)
        {
            var deletedLanguage = await sr.DeleteProduct(Id);

            if (deletedLanguage == null)
            {
                return Results.NotFound(new { Message = $"Product with Id {Id} not found."});
            }

            return TypedResults.Ok(deletedLanguage);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct(IProductRepository lr, int Id, ProductUpdatePayload payload)
        {
            try
            {
                if (payload == null)
                {
                    return Results.NotFound(new { Message = $"Product with Id {Id} not found."});
                }

                Product? product = await lr.UpdateProduct(Id, payload);

                if (product == null)
                {
                    return Results.NotFound(new { Message = $"Product with Id {Id} not found."});
                }

                return TypedResults.Ok(product);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }
    }
}