using System.Text.Json;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using exercise.wwwapi.View;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace exercise.wwwapi.Endpoint
{
    public static class ProductEndpoints
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var productsGroup = app.MapGroup("products");
            productsGroup.MapGet("/", GetProducts);

            productsGroup.MapPost("/", CreateProduct);
            productsGroup.MapGet("/{id}", GetProduct);
            productsGroup.MapPut("/{id}", UpdateProduct);
            productsGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository repo, int id)
        {
            var product = await repo.GetProduct(id);
            if (product == null)
                return TypedResults.NotFound(new { message = "Product not found." });
            await repo.DeleteProduct(id);
            return TypedResults.Ok(product);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct(HttpContext context, IProductRepository repo, int id, Product_edit dto)
        {
            var product = await repo.GetProduct(id);
            if (product == null)
                return TypedResults.NotFound(new { message = "Product not found." });

            if (dto.name != null && dto.name != product.name)
                if (await repo.GetProduct(dto.name) != null) return TypedResults.BadRequest(new { message = "Product with provided name already exists" });

            Product? data = await repo.UpdateProduct(id, dto);
            if (data == null) return TypedResults.BadRequest(new { message = "Product could not be updated" });

            string url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}/{{0}}";
            return TypedResults.Created(string.Format(url, data.id), data);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IProductRepository repo, int id)
        {
            var p = await repo.GetProduct(id);
            
            if (p == null) return TypedResults.NotFound(new { message=$"Product not found"});

            return TypedResults.Ok(p);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateProduct(HttpContext context, IProductRepository repo, Product_create dto) 
        {

            if (!dto.valid()) return TypedResults.BadRequest(new { message = "Product could not be created, invalid dto" });

            if (await repo.GetProduct(dto.name) != null) return TypedResults.BadRequest(new { message = "Product with provided name already exists" });
            
            Product? data = await repo.CreateProduct(dto);
            if (data == null)return TypedResults.BadRequest(new { message = "Product could not be created" });

            string url = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}/{{0}}";
            return TypedResults.Created(string.Format(url, data.id), data);

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IProductRepository repo, string category= "")
        {
            var datas = await repo.GetProducts(category);
            if (datas == null || datas.Count() == 0) return TypedResults.NotFound(new { message = "No products of the provided category were found"});
            return TypedResults.Ok(datas);
        }

    }
}
