using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Builder;

namespace exercise.wwwapi.Endpoint
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            //products.MapGet("/", GetAll);
            products.MapGet("/{Category?}", GetAllByCategory);
            products.MapPost("/", CreateProduct);
            products.MapGet("/GetAProduct/{Id}", GetAProduct);
            products.MapPut("/{Id}", UpdateProduct);
            products.MapDelete("/{Id}", DeleteProduct);
        }

        /*public static IResult GetAll(IRepository<Product> products)
        {
            List<Product> prod = products.GetAll();
            return prod.Any() ? TypedResults.Ok(prod) : TypedResults.NotFound("No products found");
        }*/

        public static IResult GetAllByCategory(IRepository<Product> products, string? category)
        {
            List<Product> prod = products.GetAll(category);
            return prod.Any() ? TypedResults.Ok(prod) : TypedResults.NotFound("No products of the provided category were found");
        }

        public static IResult CreateProduct(IRepository<Product> products, Product product)
        {
            try
            {
                Product prod = products.CreateProduct((Product)product);
                return TypedResults.Created("/", prod);
            }catch
            {
                return TypedResults.BadRequest("Price must be an integer, something else was provided or Product with provided name already exists.");
            }
        }

        public static IResult GetAProduct(IRepository<Product> products, int id)
        {
            Product product = products.GetProductById(id);
            return product != null ? TypedResults.Ok(product) : TypedResults.NotFound("No product found");
        }

        public static IResult UpdateProduct(IRepository<Product> products, int id, Product product)
        {
            try { 
                Product prod = products.UpdateProduct(id, product);
                return prod != null ? TypedResults.Created("/", prod) : TypedResults.NotFound("No products found");
            } catch (Exception e)
            {
                return TypedResults.BadRequest(e.Message);
            }  
        }

        public static IResult DeleteProduct(IRepository<Product> products, int id)
        {
            Product product = products.DeleteProduct(id);
            return product != null ? TypedResults.Ok(product) : TypedResults.NotFound("No product found");
        }

    }
}
