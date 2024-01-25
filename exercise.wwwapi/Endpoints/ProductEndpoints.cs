
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;

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

        public static IResult CreateProduct(IProductRepository sr, ProductPostPayload payload)
        {
            if (payload.Name == null)
                return Results.BadRequest(new { Message = "Name cannot be null" });
            if(payload.Category == null)
                return Results.BadRequest(new { Message = "Category cannot be null" });
            if(payload.Price == null)
                return Results.BadRequest(new { Message = "Price cannot be null" });
            Product product = sr.AddProduct(payload.Name, payload.Category, payload.Price);
            return TypedResults.Created($"/products/{product.Name}", product);
        }

        public static IResult GetAllProducts(IProductRepository sr)
        {
            return TypedResults.Ok(sr.GetAllProducts());
        }

        public static IResult GetProduct(IProductRepository sr, int Id)
        {
            var language = sr.GetProduct(Id);

            if (language == null)
            {
                return Results.NotFound(new { Message = $"Product with Id {Id} not found."});
            }

            return TypedResults.Ok(language);
        }

        public static IResult DeleteProduct(IProductRepository sr, int Id)
        {
            var deletedLanguage = sr.DeleteProduct(Id);

            if (deletedLanguage == null)
            {
                return Results.NotFound(new { Message = $"Product with Id {Id} not found."});
            }

            return TypedResults.Ok(deletedLanguage);
        }

        public static IResult UpdateProduct(IProductRepository lr, int Id, ProductUpdatePayload payload)
        {
            try
            {
                if (payload == null)
                {
                    return Results.BadRequest(new { Message = $"Product with Id {Id} not found."});
                }

                Product? product = lr.UpdateProduct(Id, payload);

                if (product == null)
                {
                    return Results.NotFound(new { Message = $"Product with Id {Id} not found."});
                }

                return Results.Ok(product);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }
    }
}