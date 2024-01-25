using exercise.Model.DTOs;
using exercise.Model.Models;
using exercise.Model.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace exercise.Controller.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapGet("/", GetAll);
            productGroup.MapGet("/{id}", Get);
            productGroup.MapDelete("/{id}", Delete);
            productGroup.MapPut("/{id}", Put);
            productGroup.MapPost("/", Post);
        }

        public static async Task<IResult> Post(ProductRepository context, AddProductDTO newProduct)
        {
            try
            {
                Product product = new()
                {
                    Price = newProduct.Price,
                    Name = newProduct.Name,
                    Category = newProduct.Category,
                };
                return TypedResults.Created(nameof(Post), await context.Add(product));
            }
            catch (ArgumentException ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }

        }

        public static async Task<IResult> GetAll([FromServices] ProductRepository context, [FromQuery(Name = "category")] string? category)
        {
            try
            {
                ICollection<Product> products = await context.GetAll(category);
                if (products.Count == 0) return TypedResults.NotFound("No products found");
                return TypedResults.Ok(await context.GetAll(category));
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }

        }

        public static async Task<IResult> Get(IRepository<Product> context, Guid id)
        {
            try
            {
                return TypedResults.Ok(await context.GetById(id));
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }

        public static async Task<IResult> Delete(IRepository<Product> context, Guid id)
        {
            try
            {
                return TypedResults.Ok(await context.DeleteById(id));
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }

        public static async Task<IResult> Put(ProductRepository context, Guid id, AddProductDTO newProduct)
        {
            try
            {
                Product original = await context.GetById(id);
                original.Name = newProduct.Name;
                original.Price = newProduct.Price;
                original.Category = newProduct.Category;
                await context.Update(id, original);
                return TypedResults.Ok(original);
            }
            catch (ArgumentException ex)
            {
                return TypedResults.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            } 
        }
    }
}
