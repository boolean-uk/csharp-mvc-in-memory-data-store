using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace exercise.wwwapi.Endpoints
{
    public static class ProductEndpoints
    {

        public static void ConfigureProductEndpoint(this WebApplication app)
        {
            var products = app.MapGroup("products");
            products.MapGet("/getbycategory/{category?}", GetAllProducts);
            products.MapGet("/getbyid/{id}", GetProduct);
            products.MapPut("/{id}", UpdateProduct);
            products.MapPost("/", AddProduct);
            products.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IRepository<Product> repo, string? category)
        {
            List<Product> products = repo.GetAllProducts(category);

            if(products.Count == 0)
            {
                return TypedResults.NotFound(new ErrorModel());
            }
            return TypedResults.Ok(products);

    
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetProduct(IRepository<Product> repo, int id)
        {
            Product p = repo.GetProduct(id);
            return p != null ? TypedResults.Ok(p) : TypedResults.NotFound(new ErrorModel());
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UpdateProduct(IRepository<Product> repo, int id, Product product)
        {

            if (repo.GetAllProducts(null).Any(p => p.Name == product.Name))
            {
                return TypedResults.BadRequest(new ErrorModel());
            }
            Product p = repo.UpdateProduct(id, product);

            if (p == null)
            {
                return TypedResults.NotFound(new ErrorModel());
            }
            return TypedResults.Ok(p);
            
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult AddProduct(IRepository<Product> repo, Product product)
        {
            Product p = repo.AddProduct(product);
            return p != null ? TypedResults.Created("/", p) : TypedResults.BadRequest(new ErrorModel());
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IRepository<Product> repo, int id)
        {
            Product p = repo.DeleteProduct(id);
            return p != null ? TypedResults.Ok(p) : TypedResults.NotFound(new ErrorModel());
        }
    }
}
