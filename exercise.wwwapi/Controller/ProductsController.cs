using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controller
{
    public static class ProductsController
    {
        public static void ConfigureProductsEndpoint(this WebApplication app) 
        {
            var productsGroup = app.MapGroup("products");

            productsGroup.MapPost("/", PostProduct);
            productsGroup.MapGet("/", GetProducts);
            productsGroup.MapGet("/{id}", GetSpecificProduct);
            productsGroup.MapPut("/{id}", PutProduct);
            productsGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PostProduct(IRepository repository, string name, string category, int price)
        {
            if ((name == null) || (category == null)) 
            {
                return TypedResults.BadRequest("Name and/or category must be provided.");
            }

            Product? prod = repository.PostProduct(new ProductPost(name, category, price));
            if (price < 0) // Int will never not be int. That will never happen. Checking for positive int instead
            {
                return TypedResults.BadRequest("Price must be an integer, larger than 0, something else was provided.");
            }
            if (prod == null) 
            {
                return TypedResults.BadRequest("Product with the provided name already exists.");
            }
            return TypedResults.Created($"/{prod.Id}", prod);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository, string? category) 
        {
            IEnumerable<Product?> prods = repository.GetProducts(category);
            if (prods.Count() > 0)
            {
                return TypedResults.Ok(prods);
            }
            else 
            {
                return TypedResults.NotFound("No products in the provided category were found");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetSpecificProduct(IRepository repository, int id) 
        {
            Product? prod = repository.GetSpecificProduct(id);
            if (prod != null)
            {
                return TypedResults.Ok(prod);
            }
            else 
            {
                return TypedResults.NotFound("Product not found.");
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> PutProduct(IRepository repository, int id, string? name, string? category, int price) 
        {
            Tuple<Product?, int> res = repository.PutProduct(id, new ProductPut() { Name = name, Category = category, Price = price});
            if (res.Item2 == 201)
            {
                return TypedResults.Created($"/{res.Item1.Id}", res.Item1);
            } else if (res.Item2 == 400)
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            } else 
            {
                return TypedResults.NotFound("Product not found.");
            }                    
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id) 
        {
            Product prod = repository.DeleteProduct(id);
            if (prod is null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            else 
            {
                return TypedResults.Ok(prod);
            }
        }
    }
}
