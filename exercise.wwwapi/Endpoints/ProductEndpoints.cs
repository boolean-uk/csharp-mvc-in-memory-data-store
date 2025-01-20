using System.Runtime.CompilerServices;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {

        public static void ProductEndpointsConfigure(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapPost("/", AddProduct);
            products.MapGet("/", GetAllProducts);
            products.MapGet("/{id}", GetProductById);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IProductRepository repo, string _name, string _category, int _price)
        {

            try
            {

                var product = await repo.AddProduct(new Product { Id = 0, Name = _name, Category = _category, Price = _price });
                return TypedResults.Ok(product);
            }

            catch (Exception ex)
            {
                return TypedResults.BadRequest(ex.Message);


            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAllProducts(IProductRepository repo)
        {
            
            
                var products = await repo.GetAllProducts();
            if (products == null)
            {
                return TypedResults.NotFound("No products are available");
            }
                return TypedResults.Ok(products);
            

            
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProductById(IProductRepository repo, int id)
        {
                var product = await repo.GetProductById(id);
                if (product == null)
                {
                   
                    return TypedResults.NotFound($"Product with id {id} not found.");
                }

            return TypedResults.Ok(product);

            }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IProductRepository repo, int id, string _name, string _category, int _price)
        {
            

                var product = await repo.UpdateProduct(id, _name, _category, _price);

            if (await repo.Contains(id))
            {
                return TypedResults.Ok(product);
                
            }
            return TypedResults.NotFound("The product doesnt exist in our database");


        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IProductRepository repo, int id)
        {
           
                var product = await repo.DeleteProductById(id);

            if (product == null)
            {
                return TypedResults.NotFound("The product doesnt exist");
            }
                return TypedResults.Ok(product);
            

            
        }
    }
}