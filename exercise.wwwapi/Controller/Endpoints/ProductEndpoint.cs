using exercise.wwwapi.Controller.Repository;
using exercise.wwwapi.Model.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace exercise.wwwapi.Controller.Endpoints
{
    public static class ProductEndpoint
    {
        public static void ConfigureProductEndpoint(this WebApplication application)
        {
            var productGroup = application.MapGroup("Products");
            productGroup.MapGet("/", GetAllProducts);
            productGroup.MapGet("/{id}", GetAProduct);
            productGroup.MapPost("/", AddProduct);
            productGroup.MapPut("/{id}", UppdateProduct);
            productGroup.MapDelete("/{id}", DeleteProduct);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAllProducts(IProductRepository repository)
        {
            var products = repository.GetAllProducts();
            return products is null ? TypedResults.NotFound("No products found") : TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult DeleteProduct(IProductRepository repository, int id)
        {
            var products = repository.DeleteProduct(id);
            return products is null ? TypedResults.NotFound("Product not found") : TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult GetAProduct(IProductRepository repository, int id)
        {
            var products = repository.GetAProduct(id);
            return products is null ? TypedResults.NotFound("Product not found") : TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static IResult AddProduct(IProductRepository repository, string name, string cathegory, int price)
        {
            var products = repository.AddProduct(name, cathegory, price);
            return products is null ? TypedResults.BadRequest("Bad input") : TypedResults.Ok(products);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static IResult UppdateProduct(IProductRepository repository, int id, string newname, string newcathegory, int? newprice)
        {
            var products = repository.UppdateProduct(id, newname, newcathegory, newprice);
            return products is null ? TypedResults.BadRequest("Bad input") : TypedResults.Ok(products);
        }


    }
}
