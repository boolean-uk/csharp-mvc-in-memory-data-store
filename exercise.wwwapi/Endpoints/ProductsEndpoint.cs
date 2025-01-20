using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Data;
using exercise.wwwapi.ViewModel;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductsEndpoint
    {

        public static void ConfigureProductsEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetAllProducts);
            products.MapGet("/{category}", GetProduct);
            products.MapPost("/", CreateProduct);
            products.MapDelete("/{id}", DeleteProduct);
            products.MapPut("/{id}", UpdateProduct);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAllProducts(IRepository repository, string? category=null)
        {
            
            var products = await repository.GetAllProducts(category);

            if (category == null)
            {
                
                if (products.Any())
                {
                    return TypedResults.Ok(products);
                }
                else
                {
                    
                    return TypedResults.NotFound("No products found.");
                }
            }
            else
            {
                
                if (products.Any())
                {
                   
                    return TypedResults.Ok(products);
                }
                else
                {
                    
                    return TypedResults.NotFound("No products of the provided category were found.");
                }
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {

            var product = await repository.GetProduct(id);
            if (product != null)
            {
                return TypedResults.Ok(product);

            }

            else
            {
                return TypedResults.NotFound("Product not found");
            }

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateProduct(IRepository repository,ProductPost model )
        {
            try
            {


                if (model.Price.GetType() != typeof(int))
                {
                    return TypedResults.BadRequest("Price must be an integer, something else was provided.");
                }

                var existingProduct = await repository.GetProductByName(model.Name);
                if (existingProduct != null)
                {
                    return TypedResults.BadRequest("Product with the provided name already exists.");
                }

                Product product = new Product()
                {
                    Name = model.Name,
                    Category = model.Category,
                    Price = model.Price
                };
                await repository.CreateProduct(product);

                return TypedResults.Created($"https://localhost:7188/products/{product.Id}", product);
            }
            catch (Exception)
            {
                return TypedResults.Problem("Something went wrong");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            var product = await repository.GetProduct(id);

            if (product == null)

            { 
                return TypedResults.NotFound("Product not found");

            
            }

            await repository.DeleteProduct(product.Id);

            return TypedResults.Ok(new
            {
                id = product.Id,
                name = product.Name,
                category = product.Category,
                price = product.Price
            });

        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, int id, ProductPost model)
        {
            try
            {
                if (model.Price < 0)
                {
                    return TypedResults.BadRequest("Price must be an integer");


                }

                var existingProduct = await repository.GetProductByName(model.Name);

                //Making it possible to edit parameters of the same product, but not possible to give the same name when updating another product
                if (existingProduct != null && existingProduct.Id != id)
                {
                    return TypedResults.BadRequest("Product with the provided name already exists.");
                
                }


                var productToUpdate = await repository.GetProduct(id);
                if (productToUpdate == null)
                {
                    
                    return TypedResults.NotFound(new { message = "Product not found." });
                }

                
                productToUpdate.Name = model.Name;
                productToUpdate.Category = model.Category;
                productToUpdate.Price = model.Price;

                
                await repository.UpdateProduct(productToUpdate.Id, productToUpdate);

                
                return TypedResults.Created($"https://localhost:7188/products/{productToUpdate.Id}", productToUpdate);
            }
            catch (Exception)
            {

                return TypedResults.BadRequest("Unexpected issues");
            }
        }





    }
}
