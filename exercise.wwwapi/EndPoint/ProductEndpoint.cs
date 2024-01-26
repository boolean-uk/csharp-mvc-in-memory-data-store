using exercise.wwwapi.Model;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using exercise.wwwapi.Services;

namespace exercise.wwwapi.EndPoint
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var ProductGroup = app.MapGroup("products");

            ProductGroup.MapGet("/", GetAll);
            ProductGroup.MapGet("/{id}", Get);
            ProductGroup.MapPost("/", Post);
            ProductGroup.MapPut("/{id}", Put);
            ProductGroup.MapDelete("/{id}", Delete);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAll(IRepository<InternalProduct> repository)
        {
            var products = repository.Get();
            return products != null && products.Any() ? TypedResults.Ok(products) : TypedResults.NotFound("No products available.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Get(IRepository<InternalProduct> repository, int id)
        {
            InternalProduct? product = repository.Get(id);
            return product != null ? TypedResults.Ok(product) : TypedResults.NotFound("Product not found");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> Post(IRepository<InternalProduct> repository, ProductService productService, Product product)
        {
            if (product.Price < 0)
            {
                return TypedResults.BadRequest("Price must be a positive integer.");
            }

            if (repository.NameExists(product.Name))
            {
                return TypedResults.BadRequest("Product name already exists.");
            }

            var updatedProduct = productService.CreateInternalProduct(product);
            if (updatedProduct == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            else
            {
                return TypedResults.Accepted($"url", updatedProduct);
            }
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> Put(IRepository<InternalProduct> repository, ProductService productService, int id, Product product)
        {
            if (product.Price < 0)
                return TypedResults.BadRequest("Price must be a positive integer.");

            if (repository.NameExists(product.Name))
                return TypedResults.BadRequest("Product name already exists.");

            var updatedProduct = productService.UpdateInternalProduct(id, product);
            if (updatedProduct == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            else
            {
                return TypedResults.Accepted($"url", updatedProduct);
            }
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Delete(IRepository<InternalProduct> repository, int id)
        {
            InternalProduct? product = repository.Delete(id);
            return product != null ? TypedResults.Accepted("url", product) : TypedResults.NotFound("Product not found");
        }
    }
}
