using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.OpenApi.Writers;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;

namespace exercise.wwwapi.Endpoints
{

    [ApiController]
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

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerFeature>()
                    ?.Error;
                if (exception is not null)
                {
                    var response = new { error = exception.Message };
                    context.Response.StatusCode = 400;

                    await context.Response.WriteAsJsonAsync("Price must be an integer, something else was provided.");
                }
            }));

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAllProducts(IProductRepository products, string? category)
        {

            List<Product> catss = await products.GetAllProducts(null); // .Select(x => x.Category).ToList();

            List<string> cats = catss.Select(x => x.Category).ToList();

            if (category != null && !cats.Contains(category))
            {
                return TypedResults.NotFound("No products of the provided category were found.");
            }

            List<Product> res = await products.GetAllProducts(category);

            return TypedResults.Ok(res);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IProductRepository products, int id)
        {
            Product? prod = await products.GetProduct(id);
            if (prod == null)
            {
                return TypedResults.NotFound($"Product not found.");
            }
            return TypedResults.Ok(prod);
        }

      


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateProduct([FromBody] ProductPostPayload newData,  int amount, IProductRepository products) 
        {
           
            if (newData.Name == null || newData.Category == null || newData.Price == null) return TypedResults.BadRequest("All fields are required.");
            if (newData.Name.Length == 0 || newData.Category.Length == 0 || newData.Price < 0) return TypedResults.BadRequest("No empty fields or negative prices!");

            List<Product> res = await products.GetAllProducts(null); //.Exists(x => x.Name == newData.Name);
            bool alreadyExists = res.Exists(x => x.Name == newData.Name);

            if (alreadyExists)
            {
               return TypedResults.BadRequest("Product with provided name already exists.");
            }

            Product prod = await products.AddProduct(newData.Name, newData.Category, newData.Price);

            return TypedResults.Created($"/tasks{prod.Id}", prod);
        } 



        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct(IProductRepository products, int id, ProductUpdatePayload updateData)
        {
            
            Product? prod = await products.GetProduct(id); // products.UpdateProduct(id, updateData);

            if (prod == null)
            {
               return TypedResults.NotFound($"Product not found.");
            }

            List<Product> res = await products.GetAllProducts(null); //.Where(x => x.Id != id).ToList().Exists(x => x.Name == updateData.name);
            bool alreadyExists = res.Where(x => x.Id != id).ToList().Exists(x => x.Name == updateData.name);

            if (!alreadyExists)
            {
                Product? upprod = await products.UpdateProduct(id, updateData);
                return TypedResults.Created($"/tasks{upprod.Id}", upprod);
            }

            return TypedResults.BadRequest("Product with provided name already exists.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository products, int id)
        {

            Product? prod = await products.GetProduct(id);
           

            if (prod == null)
            {
                return TypedResults.NotFound($"Product not found.");
            }

            bool res = await products.DeleteProduct(id);

            if (res == false)
            {
                return Results.NotFound("Product not found.");
            }

            return Results.Ok(prod);
        }
    }
}
