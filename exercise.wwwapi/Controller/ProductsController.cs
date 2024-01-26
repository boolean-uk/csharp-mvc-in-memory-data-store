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

            productsGroup.MapGet("/", GetProducts);
            productsGroup.MapGet("/{id}", GetSpecificProduct);
            productsGroup.MapPost("/", PostProduct).AddEndpointFilter(async (invocationContext, next) => 
            {
                var productPost = invocationContext.GetArgument<ProductPost>(1);
                if ((productPost.Name == null) || (productPost.Category == null))
                {
                    return TypedResults.BadRequest("Name and/or category must be provided.");
                } 
                else if (productPost.Price < 0) 
                {
                    return TypedResults.BadRequest("Price must be a positive integer.");
                }
                else if (productPost.Price.GetType() != typeof(int)) 
                {
                    return TypedResults.BadRequest("Price must be an integer, something else was provided.");
                }
                return await next(invocationContext);
            });
            productsGroup.MapPut("/{id}", PutProduct).AddEndpointFilter(async (invocationContext, next) =>
            {
                var productId = invocationContext.GetArgument<int>(1);
                var productPut = invocationContext.GetArgument<ProductPut>(2);
                Console.WriteLine(productPut);
                if (productPut.Price < 0)
                {
                    return TypedResults.BadRequest("Price must be a positive integer.");
                }
                else if (productPut.Price.GetType() != typeof(int))
                {
                    return TypedResults.BadRequest("Price must be an integer, something else was provided.");
                }
                return await next(invocationContext);
            });
            productsGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PostProduct(IRepository repository, ProductPost productPost)
        {
            Product? prod = repository.PostProduct(productPost);
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
                return TypedResults.NotFound("No products in the provided category were found.");
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
        public static async Task<IResult> PutProduct(IRepository repository, int id, ProductPut productPut) 
        {
            Tuple<Product?, int> res = repository.PutProduct(id, productPut);
            if (res.Item2 == 201 && res.Item1 != null)
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
            Product? prod = repository.DeleteProduct(id);
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
