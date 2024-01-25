using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.OpenApi.Writers;
using Microsoft.AspNetCore.Components.Forms;

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

            app.Use(async (ctx, next) =>
            {
                try
                {
                    await next(ctx);
                }
                catch (BadHttpRequestException)
                {
                    throw new BadHttpRequestException("Not found");

                }
            });

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IProductRepository products, string? category)
        {

            List<string> cats = products.GetAllProducts(null).Select(x => x.Category).ToList();

            if (category != null && !cats.Contains(category))
            {
                return TypedResults.NotFound("Not found.");
            }

            return TypedResults.Ok(products.GetAllProducts(category));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IProductRepository products, int id)
        {
            Product? prod = products.GetProduct(id);
            if (prod == null)
            {
                return TypedResults.NotFound($"Not found.");
            }
            return TypedResults.Ok(prod);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct( IProductRepository products, ProductPostPayload newData) 
            {
                if (newData.Name == null || newData.Category == null || newData.Price == null) return TypedResults.BadRequest("All fields are required.");
                if (newData.Name.Length == 0 || newData.Category.Length == 0 || newData.Price < 0) return TypedResults.BadRequest("No empty fields or negative prices!");

                bool alreadyExists = products.GetAllProducts(null).Exists(x => x.Name == newData.Name);
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

                bool alreadyExists = products.GetAllProducts(null).Where(x => x.Id != id).ToList().Exists(x => x.Name == updateData.name);

                if (alreadyExists)
                {
                    return TypedResults.BadRequest("Not found.");
                }

                if(updateData.price.GetType() != typeof(int))
                {
                    return TypedResults.BadRequest("Not found.");
                }

                return TypedResults.Created($"/tasks{prod.Id}", prod);
            }
            catch (Exception e)
            {
                return TypedResults.BadRequest(e.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IProductRepository products, int id)
        {

            Product? prod = products.GetProduct(id);
            bool res = products.DeleteProduct(id);

            if (prod == null)
            {
                return TypedResults.NotFound($"Not found.");
            }

            if (res == false)
            {
                return Results.NotFound("Not found.");
            }

            return Results.Ok(prod);
        }
    }
}
