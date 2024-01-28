using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Utils;
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
        public static async Task<IResult> PostProduct(IRepository<Product> repository, ProductPost productPost)
        {
            if (!ProductUtils.ProductNameIsAvailable(repository, productPost.Name))
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }
    
            Product validProduct = new(productPost.Name, productPost.Category, productPost.Price);

            Product insertedProduct = repository.Insert(validProduct);
            return TypedResults.Created($"/{insertedProduct.Id}", insertedProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository<Product> repository, string? category) 
        {
            IEnumerable<Product?> prods = repository.Get();
            if (prods.Count() < 1)
            {
                return TypedResults.NotFound("No products in the provided category were found.");
            }
            if (category == null)
            {
                return TypedResults.Ok(prods);
            }
            return TypedResults.Ok(prods.Where(e => e.Category.ToLower() == category.ToLower()).ToList());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetSpecificProduct(IRepository<Product> repository, int id) 
        {
            Product? prod = repository.GetById(id);
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
        public static async Task<IResult> PutProduct(IRepository<Product> repository, int id, ProductPut productPut) 
        {
            Product? tableEntity = repository.GetById(id);
            if (tableEntity == null) 
            {
                return TypedResults.NotFound("Product not found");
            }
            if (!ProductUtils.ProductNameIsAvailable(repository, productPut.Name)) 
            {
                return TypedResults.BadRequest("Product with provided name already exists.");
            }

            tableEntity.Name = productPut.Name ?? tableEntity.Name;
            tableEntity.Category = productPut.Category ?? tableEntity.Category;
            tableEntity.Price = productPut.Price;
            
            repository.Update(tableEntity);
            return TypedResults.Created($"/{id}", productPut);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository<Product> repository, int id) 
        {
            Product? prod = repository.Delete(id);
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
