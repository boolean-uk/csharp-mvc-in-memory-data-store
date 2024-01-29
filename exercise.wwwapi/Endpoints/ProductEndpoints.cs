using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Diagnostics;
using System.Collections.Concurrent;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetProductByID);
            productGroup.MapPost("/", CreateProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        public async static Task<IResult> GetAllProducts(IProductRepository products)
        {
            List<Product> result = await products.GetAll();
            if (result.Count == 0) return TypedResults.NotFound("Not found");
            return TypedResults.Ok(result);
        }

        public async static Task<IResult> GetProductByID(IProductRepository products, int id)
        {
            Product? result = await products.GetProductByID(id);
            if (result is null) return TypedResults.NotFound("Not found");
            return TypedResults.Ok(result);
        }

        public async static Task<IResult> CreateProduct(IProductRepository products, ProductPayload data)
        {
            try
            {
                return TypedResults.Created("", await products.Create(data));
            }
            catch
            {
                return TypedResults.BadRequest("Not found");
            }
        }

        public async static Task<IResult> UpdateProduct(IProductRepository products, int id, ProductPayload data)
        {
            try
            {
                Product? result = await products.Update(id, data);
                if (result is null) return TypedResults.NotFound("Not found");
                return TypedResults.Created("", result);
            }
            catch
            {
                return TypedResults.BadRequest("Product with that name already exists");
            }

        }

        public async static Task<IResult> DeleteProduct(IProductRepository products, int id)
        {
            try
            {
                return TypedResults.Ok(await products.Delete(id));
                
            }
            catch
            {
                return TypedResults.NotFound();
            }

        }

    }
}
