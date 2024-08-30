using exercise.wwwapi.Models.Data;
using exercise.wwwapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controller
{
    [ApiController]
    [Route("products")]
    public static class ProductController
    {
        public static void ConfigureProductController(this WebApplication app)
        {
            var products = app.MapGroup("/");
            products.MapGet("/", GetAll);
            products.MapGet("/{id}", GetProduct);
            products.MapPost("/", AddProduct);
            products.MapPut("/", UpdateProduct);
            products.MapDelete("/", DeleteProduct);
        }

        private static IResult DeleteProduct(IProductRepository repository, int id)
        {
            throw new NotImplementedException();
        }

        private static IResult UpdateProduct(IProductRepository repository, int id, Product product)
        {
            throw new NotImplementedException();
        }

        private static IResult AddProduct(IProductRepository repository, Product product)
        {
            throw new NotImplementedException();
        }

        private static IResult GetProduct(IProductRepository repository, int id)
        {
            throw new NotImplementedException();
        }

        private static IResult GetAll(IProductRepository repository)
        {
            throw new NotImplementedException();
        }
    }
}
