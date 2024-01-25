using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.OpenApi.Writers;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("products");
            group.MapGet("/", GetAllProducts);
            group.MapGet("/{id}", GetProduct);
            group.MapPost("/", CreateProduct);
            group.MapPut("/{id}", UpdateProduct);
            group.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static IResult GetAllProducts(IProductRepository products)
        {
            return TypedResults.Ok(products.GetAllProducts());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IProductRepository products, int id)
        {
            Product? prod = products.GetProduct(id);
            if (prod == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found.");
            }
            return TypedResults.Ok(prod);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IProductRepository products, ProductPostPayload newData)
        {
            int s = 3;
            
            if (newData.Price.GetType() == typeof(int) )
            {
                return TypedResults.BadRequest("Not found.");
            }
            else
            {
                Console.WriteLine(s.GetType());
                Console.WriteLine(newData.Price.GetType());
            }


            if (newData.Name == null || newData.Category == null || newData.Price == null) return TypedResults.BadRequest("All fields are required.");
            if (newData.Name.Length == 0 || newData.Category.Length == 0 || newData.Price < 0) return TypedResults.BadRequest("No empty fields or negative prices!");
            
            bool alreadyExists = products.GetAllProducts().Exists(x => x.Name == newData.Name);
            if (alreadyExists)
            {
                return TypedResults.BadRequest("Not found.");
            }



            Product prod = products.AddProduct(newData.Name, newData.Category, newData.Price);
            return TypedResults.Created($"/tasks{prod.Id}", prod);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult UpdateProduct(IProductRepository products, int id, ProductUpdatePayload updateData)
        {
            try
            {
                Product? prod = products.UpdateProduct(id, updateData);
                if (prod == null)
                {
                    return TypedResults.NotFound($"Product with id {id} not found.");
                }
                return TypedResults.Created($"/tasks{prod.Id}", prod);
            }
            catch (Exception e)
            {
                return TypedResults.BadRequest(e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IProductRepository products, int id)
        {

            Product? prod = products.GetProduct(id);

            if (prod == null)
            {
                return TypedResults.NotFound($"Product with id {id} not found.");
            }

            if (!products.DeleteProduct(id))
            {
                return Results.NotFound("Failed to delete product.");
            }
            return Results.Ok(prod);
        }
    }
}
