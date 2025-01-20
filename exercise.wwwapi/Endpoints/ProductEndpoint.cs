using exercise.wwwapi.Dto;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repositories;
using exercise.wwwapi.Validators;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoints(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("", GetProducts);
            products.MapGet("{id}", GetProduct);
            products.MapPost("", AddProduct).AddEndpointFilter<ValidationFilter<ProductDto>>();
            products.MapPut("{id}", UpdateProduct).AddEndpointFilter<ValidationFilter<ProductDto>>();
            products.MapDelete("{id}", DeleteProduct);
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProducts(IRepository repository, string category)
        {
            IEnumerable<Product> allProducts = await repository.GetProducts(category);
            var result = allProducts.Where(p => p.Category == category);
            return result.Count() > 0 ? TypedResults.Ok(result) : TypedResults.NotFound("No products of the provided category were found.");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            Product product = await repository.GetProductById(id);
            return product != null ? TypedResults.Ok(product) : TypedResults.NotFound("Product not found.");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, ProductDto product)
        {
            if (product.Price % 1 != 0)
            {
                return TypedResults.BadRequest("Price must be an integer.");
            }
            IEnumerable<Product> allProducts = await repository.GetProducts(product.Category);
            var result = allProducts.Where(p => p.Name == product.Name);
            if (result.Count() > 0)
            {
                return TypedResults.BadRequest("Product name already exists.");
            }

            Product newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Category = product.Category
            };

            Product createdProduct = await repository.AddProduct(newProduct);
            return TypedResults.Created($"https://localhost:7188/products/{newProduct.Id}", createdProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateProduct(IRepository repository, ProductDto product, int id)
        {
            if (product.Price % 1 != 0)
            {
                return TypedResults.BadRequest("Price must be an integer.");
            }
            Product target = await repository.GetProductById(id);
            if (target == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            target.Name = product.Name;
            target.Price = product.Price;
            target.Category = product.Category;
            await repository.UpdateProduct(target, id);
            return TypedResults.Created($"https://localhost:9999/products/{target.Id}", target);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            Product target = await repository.GetProductById(id);
            if (target == null)
            {
                return TypedResults.NotFound("Product not found.");
            }
            await repository.DeleteProduct(id);
            return TypedResults.Ok(target);
        }




    }
}
