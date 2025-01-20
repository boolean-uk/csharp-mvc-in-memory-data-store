using exercise.wwwapi.Models;
using exercise.wwwapi.ViewModel;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductsEndpoint
    {
        public static void ConfigureEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");

            products.MapGet("/", GetProducts);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", AddProduct);
            products.MapDelete("/{id}", DeleteProduct);
            products.MapPut("/{id}", UpdateProduct);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static async Task<IResult> GetProducts(IRepository repository, string? category)
        {
            // No category
            if (category == null)
            {
                var p = await repository.GetProducts();
                return TypedResults.Ok(p);
            }

            var products = await repository.GetProducts(category);

            if (products?.Count() == 0 || products == null)
            {
                return TypedResults.NotFound("No products of the provided category were found");
            }
            return TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetProduct(IRepository repository, int id)
        {
            try
            {
                var l = await repository.GetProducts();
                if (id > l.Count() || l == null)
                {
                    return TypedResults.NotFound("Product not found");
                }

                var p = await repository.GetProduct(id);

                return TypedResults.Ok(p);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> AddProduct(IRepository repository, AddProduct product)
        {
            try
            {   
                // if product with same name exists
                if(await repository.GetProductByName(product.name) != null)
                {
                    return TypedResults.BadRequest("Product with provided name already exists.");
                }
                Product p = new Product()
                {
                    name = product.name,
                    category = product.category,
                    price = product.price,
                };
                await repository.AddProduct(p);

            return TypedResults.Created($"https://localhost:7188/products/{p.id}", p);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> UpdateProduct(IRepository repository, AddProduct product, int id)
        {
            try
            {

                var existingProduct = await repository.GetProduct(id);

                if(existingProduct == null)
                {
                    return TypedResults.NotFound("Product not found");
                }

                // if product with same name exists
                if (await repository.GetProductByName(product.name) != null)
                {
                    return TypedResults.BadRequest("Product with provided name already exists.");
                }

                existingProduct.name = product.name;
                existingProduct.category = product.category;
                existingProduct.price = product.price;

                var p = await repository.UpdateProduct(existingProduct, id);

                return TypedResults.Created($"https://localhost:7188/products/{p.id}", p);


            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteProduct(IRepository repository, int id)
        {
            try
            {
                var p = await repository.DeleteProduct(id);
                if(p == null)
                {
                    return TypedResults.NotFound("Product not found");
                }

                return TypedResults.Ok(p);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem(ex.Message);
            }
        }

    }
}
