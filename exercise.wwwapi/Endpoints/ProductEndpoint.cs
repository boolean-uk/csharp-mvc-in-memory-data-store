using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint (this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapPost("/", CreateProduct);
            products.MapGet("/", GetAllProducts);
            products.MapGet("/{id}", GetProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult CreateProduct(IProductRepository productRepository, ProductPostModel model)
        {
            var checkName = productRepository.GetProductByName(model.Name);
            if (checkName == null) 
            {
                return TypedResults.Ok(productRepository.CreateProduct(new Product(model.Name, model.Category, model.Price)));
            }
            else
            {
                return TypedResults.BadRequest("Product with provided name already exists");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IProductRepository productRepository, string category) 
        { 
            var result = productRepository.GetAllProducts(category);
            if (result.Count > 0)
            {
                return TypedResults.Ok(productRepository.GetAllProducts(category));
            }
            else
            {
                return TypedResults.NotFound("No products of the provided category were found");
            }
        }

        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public static IResult GetProduct(IProductRepository productRepository, int id)
        {
            var result = productRepository.GetProduct(id);
            if (result != null)
            {
                return TypedResults.Ok(productRepository.GetProduct(id));
            }
            else
            {
                return TypedResults.NotFound("Product not found");
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult UpdateProduct(IProductRepository productRepository, int id, ProductPostModel model)
        {
            var product = productRepository.GetProduct(id);
            if (product != null) 
            {
                if (productRepository.GetProductByName(model.Name) == null) 
                {
                    product.Name = model.Name != null ? model.Name : product.Name;
                    product.Category = model.Category != null ? model.Category : product.Category;
                    product.Price = model.Price != 0 ? model.Price : product.Price;

                    productRepository.UpdateProduct(product);

                    return TypedResults.Ok(product);
                }
                else
                {
                    return TypedResults.BadRequest("Product with provided name already exists");
                }
            }
            else
            {
                return TypedResults.NotFound("Product not found");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IProductRepository productRepository, int id)
        {
            try 
            {
                return TypedResults.Ok(productRepository.DeleteProduct(id));
            }
            catch (Exception)
            {
                return TypedResults.NotFound("Product not found");
            }
        }
    }
}
