using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var productGroup = app.MapGroup("products");

            productGroup.MapPost("/", CreateProduct);
            productGroup.MapGet("/", GetProducts);
            productGroup.MapGet("/{id}", GetAProduct);
            productGroup.MapPut("/{id}", UpdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetProducts(IProductRepository repository, string category = null)
        {
            if (!repository.GetProducts().Any() && category == null)
            {
                return TypedResults.NotFound("No products exist.");
            }
            if (!repository.GetProducts().Any(p => p.Category == category) && category != null)
            {
                return TypedResults.NotFound("No products of the provided category were found.");
            }
            return TypedResults.Ok(repository.GetProducts(category));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAProduct(IProductRepository repository, int id)
        {
            if (repository.GetAProduct(id) == null)
            {
                return TypedResults.NotFound("The product does not exist.");
            }
            return TypedResults.Ok(repository.GetAProduct(id));
        }



        /// <summary>
        /// The commented out lines was my original solution to the integer->string problem. 
        /// It worked okay when I had ProductPost string Price, except if I removed the ' " ' in the price parameter it gave another 400 default response message. 
        /// Thanks for showing how to do it.
        /// </summary>
        [ProducesResponseType(StatusCodes.Status201Created)]

        public static async Task<IResult> CreateProduct(IProductRepository repository, ProductPost model)
        {
            if (model == null)
            {
                return TypedResults.NotFound("Product data is missing");
            }
            else if (repository.GetProducts().Any(p => p.Name == model.Name))
            {

                return TypedResults.BadRequest("Product with provided name already exists.");
            }
            else if (model.Price <= 0)
            {
                return TypedResults.BadRequest("Price must be a positive integer");
            }
            else
            {
                int newId = 1;
                if (repository.GetProducts().Any())
                {
                    newId = repository.GetProducts().Max(product => product.Id) + 1;
                }

                //var newProduct = new Product() { Id = newId, Name = model.Name, Category = model.Category, Price = IsValidPrice(model.Price) };
                var newProduct = new Product() { Id = newId, Name = model.Name, Category = model.Category, Price = model.Price };
                repository.CreateProduct(newProduct);
                return TypedResults.Created($"/{newProduct.Id}", newProduct);
            }
        }
        /*
        public static int IsValidPrice(string price)
        {
            bool priceIsInt = Int32.TryParse(price, out int actualIntValue);
            if(actualIntValue < 0 || !priceIsInt || !(price.StartsWith("\"") && price.EndsWith("\"")))
            {
                return -1;
            }
            else
            {
                return actualIntValue;
            }
            
        }
        */


        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> UpdateProduct(IProductRepository repository, int id, ProductPut model)
        {
            if (repository.GetAProduct(id) == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            else if (model.Price <= 0)
            {
                return TypedResults.BadRequest("Price must be a positive integer");
            }
            return TypedResults.Ok(repository.UpdateProduct(id, model));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> DeleteProduct(IProductRepository repository, int id)
        {
            if (repository.GetAProduct(id) == null)
            {
                return TypedResults.NotFound("The product does not exist.");
            }

            return TypedResults.Ok(repository.DeleteProduct(id));

        }
    }
}
